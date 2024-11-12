using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductDisplayController : Controller
    {
        private readonly ProductDisplay _productDisplay;

        public ProductDisplayController(ProductDisplay productDisplay)
        {
            _productDisplay = productDisplay;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var products = _productDisplay.SelectProducts();
            return View(products);
        }
    }
}
