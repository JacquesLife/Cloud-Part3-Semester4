using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginModel _loginModel;

        public LoginController(IConfiguration configuration)
        {
            _loginModel = new LoginModel(configuration);
        }

        [HttpPost]
        public ActionResult Privacy(string email, string name)
        {
            int userID = _loginModel.SelectUser(email, name);
            if (userID != -1)
            {
                return RedirectToAction("Index", "Home", new { userID = userID });
            }
            else
            {
                return View("LoginFailed");
            }
        }
    }
}