using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private readonly UserTable _usrtbl;

        public UserController(IConfiguration configuration)
        {
            _usrtbl = new UserTable(configuration);
        }

        [HttpPost]
        public ActionResult About(string name, string surname, string email)
        {
            var result = _usrtbl.InsertUser(name, surname, email);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult About()
        {
            return View(_usrtbl);
        }
    }
}
