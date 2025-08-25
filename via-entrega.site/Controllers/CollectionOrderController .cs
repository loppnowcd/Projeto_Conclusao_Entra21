using Microsoft.AspNetCore.Mvc;
using via_entrega.interfaces.Services;
using via_entrega.services;
using ViewModels.CollectionOrder;

namespace ViaEntrega.Web.Controllers
{
	[Route("entregas")]
	public class CollectionOrderController : Controller
	{
		private readonly ICollectionOrderService _collectionOrderService;

		public CollectionOrderController(ICollectionOrderService collectionOrderService)
		{
			_collectionOrderService = collectionOrderService;
		}


		public IActionResult Index()
		{
			return View();
		}

			[HttpGet("criar")]
		

		// Submit tradicional (form-urlencoded / multipart)
		[HttpPost("criar")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateAsync(CollectionOrderViewModel collectionOrder)
		{
			// Validações adicionais de negócio
			ValidarProdutos(collectionOrder);

			if (!ModelState.IsValid)
				return View(collectionOrder);

			// Mapeie para sua entidade de domínio e salve
			var idEntrega = await _collectionOrderService.CreateAsync(collectionOrder);

			TempData["Sucesso"] = "Entrega registrada com sucesso!";
			return RedirectToAction(nameof(Details), new { id = idEntrega });
		}

		
		

		[HttpGet("{id:long}")]
		public async Task<IActionResult> Details()
		{
			var entrega = await _collectionOrderService.GetAllAsync();
			if (entrega is null) return NotFound();
			return View(entrega); // pode ser um ViewModel de detalhes
		}

		private void ValidarProdutos(CollectionOrderViewModel vm)
		{
			if (vm.Produtos is null || vm.Produtos.Count == 0)
				ModelState.AddModelError(nameof(vm.Produtos), "Inclua ao menos um produto.");

			if (vm.Produtos != null)
			{
				foreach (var (p, idx) in vm.Produtos.Select((p, i) => (p, i)))
				{
					if (p.AlturaCm <= 0 || p.LarguraCm <= 0 || p.ComprimentoCm <= 0)
						ModelState.AddModelError($"Produtos[{idx}]", "Dimensões devem ser maiores que zero.");

					if (p.PesoKg <= 0)
						ModelState.AddModelError($"Produtos[{idx}]", "Peso deve ser maior que zero.");

					if (p.Quantidade <= 0)
						ModelState.AddModelError($"Produtos[{idx}]", "Quantidade deve ser maior que zero.");
				}
			}
		}
	}

	
}
