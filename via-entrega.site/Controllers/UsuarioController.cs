using Microsoft.AspNetCore.Mvc;
using via_entrega.entities.Registrations;
using via_entrega.interfaces.Services;
using via_entrega.services;
using ViewModels;

namespace Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly ILogger<UsuarioController> _logger;
        public UsuarioController(ILogger<UsuarioController> logger, IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Cadastrar()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Cadastrar(UsuarioViewModel? usuarioViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (usuarioViewModel.Senha != usuarioViewModel.ConfirmarSenha)
                    {
                        TempData["MensagemErro"] = "As senhas não conferem.";
                        return View(usuarioViewModel);
                    }

                    Usuario usuario = new Usuario
                    {
                        Email = usuarioViewModel?.Email,
                        Senha = usuarioViewModel?.Senha,
                    };

                    Guid? result = await _usuarioService.CriarAsync(usuario);
                    if (result.HasValue && result.Value != Guid.Empty)
                    {
                        TempData["MensagemSucesso"] = "Usuário criado com sucesso!";
                        return RedirectToAction("Index");
                    }
                }

                TempData["MensagemErro"] = "Ocorreu um erro na criação do usuário.";
                return View(usuarioViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao criar novo usuário {usuarioViewModel?.Email}");
                TempData["MensagemErro"] = "Erro interno ao criar novo usuário.";
                return View(usuarioViewModel);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Atualizar(UsuarioViewModel? usuarioViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario? result = await _usuarioService.AtualizarAsync(new Usuario());
                    if (result is not null)
                    {
                        TempData["MensagemSucesso"] = "Usuário atualizado com sucesso!";
                        return RedirectToAction("Index");
                    }
                }
                TempData["MensagemErro"] = "Erro ao atualizar o usuário.";
                return View(usuarioViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar usuário");
                TempData["MensagemErro"] = "Erro interno ao atualizar usuário.";
                return View(usuarioViewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Desativar(Guid id)
        {
            try
            {
                Usuario? result = await _usuarioService.AtualizarAsync(new Usuario());
                if (result is not null)
                {
                    TempData["MensagemSucesso"] = "Usuário desativado com sucesso!";
                    return RedirectToAction("Index");
                }
                TempData["MensagemErro"] = "Erro ao desativar o usuário.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao desativar usuário");
                TempData["MensagemErro"] = "Erro interno ao desativar usuário.";
                return RedirectToAction("Index");
            }
        }
    }
}
