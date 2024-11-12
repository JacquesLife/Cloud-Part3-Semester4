using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        public ProductTable product;

        public ProductController(IConfiguration configuration)
        {
            product = new ProductTable(configuration);
        }

        [HttpPost]
        public ActionResult Work(ProductTable products)
        {
            var result = product.InsertProduct(products);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Work()
        {
            return View();
        }
    }
}