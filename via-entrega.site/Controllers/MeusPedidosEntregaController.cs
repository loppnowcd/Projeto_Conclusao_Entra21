using Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using via_entrega.entities.Enums;
using via_entrega.interfaces.Services;
using ViewModels.PessoaFisica;

namespace Controllers
{
	[Authorize]
	public class MeusPedidosEntregaController : Controller
	{
		private readonly IDeliveryOrderService _deliveryOrderService;
		private readonly ILogger<MeusPedidosEntregaController> _logger;

		public MeusPedidosEntregaController(IDeliveryOrderService deliveryOrderService, ILogger<MeusPedidosEntregaController> logger)
		{
			_deliveryOrderService = deliveryOrderService;
			_logger = logger;
		}

		// Página principal
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public async Task<IActionResult> MeusPedidosEntrega()
		{
			return await List();
		}
		// Lista parcial dos pedidos de entrega do usuário logado
		[HttpGet]
		public async Task<IActionResult> List()
		{
			try
			{
				var pessoaId = HttpContext.GetPessoaId();

				if (pessoaId == null || pessoaId == Guid.Empty)
				{
					_logger.LogWarning("Tentativa de acesso aos pedidos sem pessoa identificada");
					return PartialView("_MeusPedidosEntrega", new List<MeusPedidosEntregaViewModel>());
				}

				var pedidosEntrega = await _deliveryOrderService.BuscarOrdersPelaPessoa(pessoaId.Value);

				if (pedidosEntrega == null || !pedidosEntrega.Any())
				{
					return PartialView("_MeusPedidosEntrega", new List<MeusPedidosEntregaViewModel>());
				}

				var pedidosViewModel = pedidosEntrega.Select(pedido => new MeusPedidosEntregaViewModel
				{
					DataColeta = pedido.DataColeta,
					DataEntrega = pedido.DataEntrega,
					Status = pedido.Status,
					NomeCliente = pedido.PessoaFisica?.Nome ?? pedido.PessoaJuridica?.Nome ?? "Cliente não informado"
				}).ToList();

				return PartialView("_MeusPedidosEntrega", pedidosViewModel);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro ao carregar pedidos de entrega para pessoa ID: {PessoaId}", HttpContext.GetPessoaId());

				// Em caso de erro, retorna dados fictícios para desenvolvimento
				// TODO: Remover dados fictícios em produção
				var dadosFicticios = ObterDadosFicticios();
				return PartialView("_MeusPedidosEntrega", dadosFicticios);
			}
		}

		// Buscar pedido específico por ID
		[HttpGet]
		public async Task<IActionResult> GetById(Guid id)
		{
			try
			{
				var pedido = await _deliveryOrderService.GetByIdAsync(id);

				if (pedido == null)
				{
					return NotFound("Pedido não encontrado");
				}

				// Verifica se o pedido pertence ao usuário logado
				var pessoaId = HttpContext.GetPessoaId();
				if (pedido.PessoaFisicaId != pessoaId && pedido.PessoaJuridicaId != pessoaId)
				{
					return Forbid("Acesso negado ao pedido");
				}

				var pedidoViewModel = new MeusPedidosEntregaViewModel
				{
					DataColeta = pedido.DataColeta,
					DataEntrega = pedido.DataEntrega,
					Status = pedido.Status,
					NomeCliente = pedido.PessoaFisica?.Nome ?? pedido.PessoaJuridica?.Nome ?? "Cliente não informado"
				};

				return Json(pedidoViewModel);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro ao buscar pedido por ID: {Id}", id);
				return StatusCode(500, "Erro interno do servidor");
			}
		}

		// Filtrar pedidos por status
		[HttpGet]
		public async Task<IActionResult> FilterByStatus(Status? status)
		{
			try
			{
				var pessoaId = HttpContext.GetPessoaId();

				if (pessoaId == null || pessoaId == Guid.Empty)
				{
					return PartialView("_MeusPedidosEntrega", new List<MeusPedidosEntregaViewModel>());
				}

				var pedidosEntrega = await _deliveryOrderService.BuscarOrdersPelaPessoa(pessoaId.Value);

				if (pedidosEntrega == null || !pedidosEntrega.Any())
				{
					return PartialView("_MeusPedidosEntrega", new List<MeusPedidosEntregaViewModel>());
				}

				var pedidosFiltrados = pedidosEntrega.AsQueryable();

				if (status.HasValue)
				{
					pedidosFiltrados = pedidosFiltrados.Where(p => p.Status == status.Value);
				}

				var pedidosViewModel = pedidosFiltrados.Select(pedido => new MeusPedidosEntregaViewModel
				{
					DataColeta = pedido.DataColeta,
					DataEntrega = pedido.DataEntrega,
					Status = pedido.Status,
					NomeCliente = pedido.PessoaFisica.Nome ?? pedido.PessoaJuridica.Nome ?? "Cliente não informado"
				}).ToList();

				return PartialView("_MeusPedidosEntrega", pedidosViewModel);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro ao filtrar pedidos por status: {Status}", status);
				return PartialView("_MeusPedidosEntrega", new List<MeusPedidosEntregaViewModel>());
			}
		}

		// Cancelar pedido (se permitido pelo status atual)
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CancelarPedido(Guid id)
		{
			try
			{
				var pedido = await _deliveryOrderService.GetByIdAsync(id);

				if (pedido == null)
				{
					return Json(new { success = false, message = "Pedido não encontrado" });
				}

				// Verifica se o pedido pertence ao usuário logado
				var pessoaId = HttpContext.GetPessoaId();
				if (pedido.PessoaFisicaId != pessoaId && pedido.PessoaJuridicaId != pessoaId)
				{
					return Json(new { success = false, message = "Acesso negado ao pedido" });
				}

				// Verifica se o pedido pode ser cancelado (apenas se estiver aguardando coleta)
				if (pedido.Status != Status.AguardandoColeta)
				{
					return Json(new { success = false, message = "Pedido não pode ser cancelado no status atual" });
				}

				pedido.Status = Status.Cancelado;
				pedido.UpdatedAt = DateTime.UtcNow;

				var resultado = await _deliveryOrderService.UpdateAsync(pedido);

				if (resultado == null)
				{
					return Json(new { success = false, message = "Erro ao cancelar o pedido" });
				}

				return Json(new { success = true, message = "Pedido cancelado com sucesso" });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Erro ao cancelar pedido: {Id}", id);
				return Json(new { success = false, message = "Erro interno do servidor" });
			}
		}

		// Método privado para dados fictícios (desenvolvimento)
		private List<MeusPedidosEntregaViewModel> ObterDadosFicticios()
		{
			return new List<MeusPedidosEntregaViewModel>
			{
				new MeusPedidosEntregaViewModel
				{
					DataColeta = DateTime.Now.AddDays(-2),
					DataEntrega = DateTime.Now.AddDays(1),
					Status = Status.Cancelado,
					NomeCliente = "João da Silva"
				},
				new MeusPedidosEntregaViewModel
				{
					DataColeta = DateTime.Now.AddDays(-5),
					DataEntrega = DateTime.Now.AddDays(2),
					Status = Status.Entregue,
					NomeCliente = "Maria Oliveira"
				},
				new MeusPedidosEntregaViewModel
				{
					DataColeta = DateTime.Now.AddDays(-10),
					DataEntrega = DateTime.Now.AddDays(3),
					Status = Status.AguardandoColeta,
					NomeCliente = "Carlos Pereira"
				},
				new MeusPedidosEntregaViewModel
				{
					DataColeta = DateTime.Now.AddDays(-1),
					DataEntrega = DateTime.Now.AddDays(4),
					Status = Status.AguardandoColeta,
					NomeCliente = "Ana Souza"
				},
				new MeusPedidosEntregaViewModel
				{
					DataColeta = DateTime.Now.AddDays(-3),
					DataEntrega = DateTime.Now.AddDays(5),
					Status = Status.Cancelado,
					NomeCliente = "Roberto Santos"
				}
			};
		}
	}
}