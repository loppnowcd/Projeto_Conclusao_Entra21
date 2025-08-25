using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using via_entrega.entities.Enums;
using via_entrega.interfaces.Services;
using ViewModels.PessoaFisica;

namespace Controllers
{
	public class PessoaFisicaController : Controller
	{
		private readonly IDeliveryOrderService _deliveryOrderService;
		public PessoaFisicaController(IDeliveryOrderService deliveryOrderService)
		{
			_deliveryOrderService = deliveryOrderService;
		}
		public IActionResult Index()
		{
			return View();
		}

		// GET: PessoaFisica/ListarMeusPedidosEntrega
		public async Task<IActionResult> ListarMeusPedidosEntrega()
		{
			// tenta obter o id do usuário logado nas claims
			var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

			// quando não houver login ou claim inválida, não quebra — apenas retorna parcial vazia
			if (string.IsNullOrWhiteSpace(idClaim) || !Guid.TryParse(idClaim, out var pessoaId))
			{
				return PartialView("_MeusPedidosEntrega", model: null);
			}

			pessoaId = new Guid();

			// busca a ordem pelo id da pessoa (pode retornar null)
			var pedidoEntrega = await _deliveryOrderService.BuscarOrdersPelaPessoa(pessoaId);

			if (pedidoEntrega.Any())
			{
				return PartialView("_MeusPedidosEntrega", model: null);
			}



			//var deliveryOrderDashboard = new MeusPedidosEntregaViewModel
			//{
			//	DataColeta = pedidoEntrega.DataColeta,
			//	DataEntrega = pedidoEntrega.DataEntrega,
			//	Status = pedidoEntrega.Status,
			//	NomeCliente = pedidoEntrega.PessoaFisica is null
			//				  ? pedidoEntrega.PessoaJuridica?.Nome
			//				  : pedidoEntrega.PessoaFisica?.Nome
			//};

			IEnumerable<MeusPedidosEntregaViewModel?>  dados = MontarDadosFicticios();

			return PartialView("_MeusPedidosEntrega", dados);
		}

		private IEnumerable<MeusPedidosEntregaViewModel?> MontarDadosFicticios()
		{
			List<MeusPedidosEntregaViewModel?> pedidos = new List<MeusPedidosEntregaViewModel?>
			{
				new MeusPedidosEntregaViewModel
				{
					DataColeta = DateTime.Now.AddDays(-2),
					DataEntrega = DateTime.Now.AddDays(1),
					Status = via_entrega.entities.Enums.Status.Cancelado,
					NomeCliente = "João da Silva"
				},
				new MeusPedidosEntregaViewModel
				{
					DataColeta = DateTime.Now.AddDays(-5),
					DataEntrega = DateTime.Now.AddDays(2),
					Status = Status.Entregue,
					NomeCliente = "Maria Oliveira",
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

			return pedidos;
		}
	}

}
