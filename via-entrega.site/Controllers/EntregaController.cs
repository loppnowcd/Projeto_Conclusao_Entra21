using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using via_entrega.entities.Orders;
using via_entrega.services;
using ViewModels.Pedidos;
using System.ComponentModel.DataAnnotations;

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
        public async Task<IActionResult> StatusEntrega()
        {
            return View();
        
        }


		[HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var collectionOrders = await _collectionOrderService.GetAllIncludedAsync();

                var entregaViewModels = collectionOrders.Select(order => new EntregaResumoViewModel
                {
                    Id = order.Id,
                    Destinatario = order.Destinatario,
                    Endereco = order.Endereco,
                    Telefone = order.Telefone
                }).ToList();

                return View(entregaViewModels);
            }
            catch (Exception ex)
            {
                // Log do erro aqui
                TempData["Erro"] = "Erro ao carregar as entregas. Tente novamente.";
                return View(new List<EntregaResumoViewModel>());
            }
        }

        [HttpGet]
        public IActionResult Criar()
        {
            var viewModel = new EntregaViewModel();
            return PartialView("_EntregaForm", viewModel);
        }

        [HttpGet]
        public IActionResult Editar(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("ID inválido");
                }

                var entrega = _collectionOrderService.GetByIdIncluded(id);
                if (entrega == null)
                {
                    return NotFound("Entrega não encontrada");
                }

                var viewModel = new EntregaViewModel
                {
                    Id = entrega.Id,
                    Endereco = entrega.Endereco,
                    Destinatario = entrega.Destinatario,
                    Telefone = entrega.Telefone,
                    Documento = entrega.Documento,
                    Observacao = entrega.Observacao,
                    Produtos = entrega.Products?.Select(p => ProdutoViewModel.ConverterParaViewModel(p)).ToList() ?? new List<ProdutoViewModel>()
                };

                return PartialView("_EntregaForm", viewModel);
            }
            catch (Exception ex)
            {
                // Log do erro aqui
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar([FromForm] EntregaViewModel model)
        {
            try
            {
                // Validação manual dos produtos se necessário
                if (!string.IsNullOrEmpty(model.ProdutosJson))
                {
                    try
                    {
                        model.Produtos = JsonConvert.DeserializeObject<List<ProdutoViewModel>>(model.ProdutosJson) ?? new List<ProdutoViewModel>();
                    }
                    catch (JsonException)
                    {
                        ModelState.AddModelError("", "Dados dos produtos inválidos");
                        return PartialView("_EntregaForm", model);
                    }
                }


                if (!ModelState.IsValid)
                {
                    return PartialView("_EntregaForm", model);
                }

                var collectionOrder = model.ConverterParaEntidade();
                var resultado = await _collectionOrderService.CreateAsync(collectionOrder);

                if (resultado == null)
                {
                    ModelState.AddModelError("", "Erro ao criar a ordem de coleta. Tente novamente.");
                    return PartialView("_EntregaForm", model);
                }
                TempData["MensagemSucesso"] = "Produto Cadastrado com Sucesso!!";
				//return Json(new { success = true, message = "Entrega criada com sucesso!" });
				return RedirectToAction("Index", "Entrega");
			}
			catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                TempData["MensagemErro"] = ex.Message;
                return RedirectToAction("Index", "Entrega");
			}
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ex.Message;
				// Log do erro aqui
				ModelState.AddModelError("", "Erro interno. Contate o suporte.");
                return RedirectToAction("Index", "Entrega");
			}
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar([FromForm] EntregaViewModel model)
        {
            try
            {
                if (model.Id == Guid.Empty)
                {
                    return BadRequest("ID inválido");
                }

                // Buscar a entrega existente
                var entregaExistente = _collectionOrderService.GetByIdIncluded(model.Id);
                if (entregaExistente == null)
                {
                    return NotFound("Entrega não encontrada");
                }

                // Validação dos produtos
                if (!string.IsNullOrEmpty(model.ProdutosJson))
                {
                    try
                    {
                        model.Produtos = JsonConvert.DeserializeObject<List<ProdutoViewModel>>(model.ProdutosJson) ?? new List<ProdutoViewModel>();
                    }
                    catch (JsonException)
                    {
                        ModelState.AddModelError("", "Dados dos produtos inválidos");
                        return PartialView("_EntregaForm", model);
                    }
                }


                if (!ModelState.IsValid)
                {
                    return PartialView("_EntregaForm", model);
                }

                // Atualizar propriedades
                entregaExistente.Endereco = model.Endereco;
                entregaExistente.Destinatario = model.Destinatario;
                entregaExistente.Telefone = model.Telefone;
                entregaExistente.Documento = model.Documento;
                entregaExistente.Observacao = model.Observacao;
                entregaExistente.UpdatedAt = DateTime.UtcNow;

                // TODO: Implementar lógica para atualizar produtos se necessário
                // Isso depende de como você quer tratar a edição de produtos

                var resultado = await _collectionOrderService.UpdateAsync(entregaExistente);

                if (resultado == null)
                {
                    ModelState.AddModelError("", "Erro ao atualizar a entrega. Tente novamente.");
                    return PartialView("_EntregaForm", model);
                }

                return Json(new { success = true, message = "Entrega atualizada com sucesso!" });
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return PartialView("_EntregaForm", model);
            }
            catch (Exception ex)
            {
                // Log do erro aqui
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return Json(new { success = false, message = "ID inválido" });
                }

                var entrega = await _collectionOrderService.GetByIdAsync(id);
                if (entrega == null)
                {
                    return Json(new { success = false, message = "Entrega não encontrada" });
                }

                var resultado = await _collectionOrderService.DeleteAsync(id);

                if (!resultado)
                {
                    return Json(new { success = false, message = "Erro ao excluir a entrega" });
                }

                return Json(new { success = true, message = "Entrega excluída com sucesso!" });
            }
            catch (Exception ex)
            {
                // Log do erro aqui
                return Json(new { success = false, message = "Erro interno do servidor" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detalhes(Guid id)
        {
            try
            {
                if (id == Guid.Empty)
                {
                    return BadRequest("ID inválido");
                }

                var entrega = _collectionOrderService.GetByIdIncluded(id);
                if (entrega == null)
                {
                    return NotFound("Entrega não encontrada");
                }

                var viewModel = EntregaViewModel.ConverterParaViewModel(entrega);
                return PartialView("_EntregaDetalhes", viewModel);
            }
            catch (Exception ex)
            {
                // Log do erro aqui
                return StatusCode(500, "Erro interno do servidor");
            }
        }

        #region Métodos Privados

        // Métodos auxiliares podem ser adicionados aqui conforme necessário

        #endregion
    }
}