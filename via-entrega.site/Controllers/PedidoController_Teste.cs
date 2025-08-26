using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using via_entrega.entities.Enums;
using via_entrega.entities.Orders;
using via_entrega.interfaces.Services;
using via_entrega.services; // para ICollectionOrderService se necessário
using ViewModels.Orders;

namespace Controllers
{
	[Authorize] // se ainda não usa auth, pode remover; mas mantém a ideia
	public class PedidosController : Controller
	{
		private readonly IDeliveryOrderService _deliverySvc;
		private readonly ICollectionOrderService _collectionSvc;

		public PedidosController(
			IDeliveryOrderService deliverySvc,
			ICollectionOrderService collectionSvc)
		{
			_deliverySvc = deliverySvc;
			_collectionSvc = collectionSvc;
		}

		// GET: /Pedidos
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			// Tenta obter o Guid da pessoa a partir das claims (sem quebrar se vier nulo/ inválido)
			Guid? pessoaId = null;

			var rawClaim = User?.FindFirst("PessoaId")?.Value
						  ?? User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
						  ?? User?.FindFirst("sub")?.Value;

			if (Guid.TryParse(rawClaim, out var guidOk))
				pessoaId = guidOk;

			// Se não temos pessoa, retorna lista vazia (estado vazio na View).
			var pedidos = new List<OrderListItemVM>();

			if (pessoaId.HasValue)
			{
				var orders = await _deliverySvc.BuscarOrdersPelaPessoa(pessoaId.Value);
				if (orders != null)
				{
					pedidos = orders
						.Where(o => o != null)
						.Select(o => new OrderListItemVM
						{
							Id = o!.Id,
							DataColeta = o.DataColeta,
							DataEntrega = o.DataEntrega,
							Status = o.Status.ToString(),
							Endereco = o.CollectionOrder?.Endereco,
							Destinatario = o.CollectionOrder?.Destinatario,
							Telefone = o.CollectionOrder?.Telefone,
							ValorTotal = o.CollectionOrder?.ValorTotal
						})
						.ToList();
				}
			}

			return View(pedidos);
		}

		// GET: /Pedidos/Create
		[HttpGet]
		public IActionResult Create()
		{
			return View(new OrderCreateVM());
		}

		// POST: /Pedidos/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(OrderCreateVM vm)
		{
			if (!ModelState.IsValid) return View(vm);

			// tenta pegar PessoaId (pode ser nulo: entidades permitem PessoaFisicaId/PessoaJuridicaId nulos)
			Guid? pessoaId = null;
			var rawClaim = User?.FindFirst("PessoaId")?.Value
						  ?? User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
						  ?? User?.FindFirst("sub")?.Value;
			if (Guid.TryParse(rawClaim, out var guidOk))
				pessoaId = guidOk;

			try
			{
				// 1) cria CollectionOrder (requerido pois DeliveryOrder tem FK obrigatória)
				var collection = new CollectionOrder
				{
					PessoaFisicaId = pessoaId, // ou PessoaJuridicaId se for o caso
					Endereco = vm.Endereco,
					Destinatario = vm.Destinatario ?? "",
					Telefone = vm.Telefone ?? "",
					Documento = vm.Documento ?? "",
					Observacao = vm.Observacao ?? "",
					ValorTotal = vm.ValorTotal ?? 0m,
					
				};

				var collectionId = await _collectionSvc.CreateAsync(collection);
				if (collectionId is null)
				{
					ModelState.AddModelError("", "Não foi possível criar a ordem de coleta.");
					return View(vm);
				}

				// 2) cria DeliveryOrder apontando para a CollectionOrder criada
				var delivery = new DeliveryOrder
				{
					PessoaFisicaId = pessoaId,
					CollectionOrderId = collectionId.Value,
					Observacao = vm.Observacao ?? "",
					DataColeta = vm.DataColeta,
					DataEntrega = vm.DataEntrega,
					Status = Status.AguardandoColeta,
				};

				var novoId = await _deliverySvc.CreateAsync(delivery);
				if (novoId is null)
				{
					ModelState.AddModelError("", "Não foi possível criar o pedido de entrega.");
					return View(vm);
				}

				TempData["Sucesso"] = "Solicitação criada com sucesso!";
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				ModelState.AddModelError("", $"Erro ao salvar: {ex.Message}");
				return View(vm);
			}
		}
	}
}
