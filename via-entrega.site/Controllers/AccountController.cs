using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using via_entrega.interfaces.Services;
using ViewModels;
using ViewModels.Cadastro;

namespace Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUsuarioService _usuarioService;
        public AccountController(ILogger<AccountController> logger, IUsuarioService usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }


        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(!await ValidateCaptcha())
            {
                ModelState.AddModelError("", "Captcha inválido");
                return View("Index", model);
            }

            if (ModelState.IsValid)
            {
                // Validar credenciais (exemplo simples)
                ClaimsPrincipal? claimsPrincipal = await _usuarioService.Login(model.Email, model.Senha);
                if (claimsPrincipal is null)
                {
                    ModelState.AddModelError("", "Credenciais inválidas");
                    return View("Index", model);
                }

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                return RedirectToAction("Index", "Home");
            }

            return View("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        private async Task<bool> ValidateCaptcha()
        {
            try
            {
                HttpClient cliente = new HttpClient();
                cliente.BaseAddress = new Uri("https://api.hcaptcha.com/siteverify");
                cliente.Timeout = TimeSpan.FromSeconds(30);
                var request = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        { "secret", "ES_4eb4104446394164a58fbc1ca43bf0c6" },
                        { "response", Request.Form["h-captcha-response"] }
                    })
                };

                HttpResponseMessage httpResponse = await cliente.SendAsync(request);

                if (httpResponse.IsSuccessStatusCode)
                {
                    string result = await httpResponse.Content.ReadAsStringAsync();

                    HCaptchaResponse captchaResponse = JsonSerializer.Deserialize<HCaptchaResponse>(result);

                    if (captchaResponse is not null && captchaResponse.success)
                        return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}