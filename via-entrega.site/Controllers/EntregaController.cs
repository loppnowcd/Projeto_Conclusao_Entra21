using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using via_entrega.entities.Orders;
using via_entrega.services;
using ViewModels.Pedidos;

namespace Controllers
{
    public class EntregaController : Controller
    {
        private readonly ICollectionOrderService _collectionOrderService;


        public EntregaController(ICollectionOrderService collectionOrderService)
        {
            _collectionOrderService = collectionOrderService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            List<CollectionOrder> collectionOrders = _collectionOrderService.GetAllIncludedAsync().Result;

            List<EntregaResumoViewModel> entregaViewModels = [];


            foreach (CollectionOrder collectionOrder in collectionOrders)
            {
                entregaViewModels.Add(new EntregaResumoViewModel()
                {
                    Destinatario = collectionOrder.Destinatario,
                    Endereco = collectionOrder.Endereco,
                    Telefone = collectionOrder.Telefone,
                    Id = collectionOrder.Id
                });
            }  

            return View(entregaViewModels);
        }

        //[HttpPost]
        //public async Task<IActionResult> Criar(EntregaViewModel model, string ProdutosJson)
        //{

        //    if (!string.IsNullOrEmpty(ProdutosJson))
        //    {
        //        model.Produtos = JsonConvert.DeserializeObject<List<ProdutoViewModel>>(ProdutosJson);
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }


        //    Guid? idCollectionOrder = await _collectionOrderService.CreateAsync(model.ConverterParaEntidade());

        //    if (idCollectionOrder is null)
        //    {
        //        ModelState.AddModelError(string.Empty, "Erro ao criar a ordem de coleta.");
        //        return View(model);
        //    }

        //    return RedirectToAction("Index");
        //}


        public IActionResult Criar()
        {
            return PartialView("_EntregaForm", new EntregaViewModel());
        }

        public IActionResult Editar(Guid id)
        {
            var entrega = _collectionOrderService.GetByIdIncluded(id);
            if (entrega == null) return NotFound();

            var viewModel = new EntregaViewModel
            {
                // mapeamento aqui
                Endereco = entrega.Endereco,
                Destinatario = entrega.Destinatario,
                Telefone = entrega.Telefone,
                Documento = entrega.Documento,
                Observacao = entrega.Observacao,
                ProdutosJson = JsonConvert.SerializeObject(entrega.Products)
            };

            return PartialView("_EntregaForm", viewModel);
        }

        [HttpPost]
        public IActionResult Criar(EntregaViewModel model)
        {
            if (!ModelState.IsValid)
                return PartialView("_EntregaForm", model);

            var produtos = JsonConvert.DeserializeObject<List<ProdutoViewModel>>(model.ProdutosJson);
            _collectionOrderService.CreateAsync(model.ConverterParaEntidade());

            return Json(new { success = true });
        }

    }
}