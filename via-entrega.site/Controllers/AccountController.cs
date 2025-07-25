using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace via_entrega.site.Controllers
{
	public class AccountController : Controller
	{
		private readonly ILogger<AccountController> _logger;

		public AccountController(ILogger<AccountController> logger)
		{
			_logger = logger;
		}

		public async Task Login(string returnUrl = "/")
		{
			var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
				.WithRedirectUri(returnUrl)
				.Build();

			await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
		}

		[Authorize]
		public async Task Logout()
		{
			var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
				.WithRedirectUri(Url.Action("Index", "Home"))
				.Build();

			await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		}

		public IActionResult Index()
		{
			return View();
		}
	}
}
