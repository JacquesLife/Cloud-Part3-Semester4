using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WebApplication1.Models;
using Microsoft.Extensions.Configuration;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index(int userID)
        {
            ProductTable product = new ProductTable(_configuration);
            var products = product.GetAllProducts();

            ViewData["products"] = products;
            ViewData["userID"] = userID;

            return View();
        }

            public IActionResult Multimedia()
        {
            return View();
        }

        public IActionResult FileShare()
        {
            return View();
        }

        public IActionResult Tables()
        {
            return View();
        }

        public IActionResult Queue()
        {
            return View();
        }

        public IActionResult ProductManagement()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
