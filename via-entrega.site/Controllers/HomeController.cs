using Azure;

using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
		public IActionResult Inicio()
		{
			return View();
		}

		public IActionResult Index()
        {
            if (Request.HttpContext.User.Identity.IsAuthenticated)
                return View("Inicio");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


  
    }
}
