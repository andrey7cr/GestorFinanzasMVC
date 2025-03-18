using Microsoft.AspNetCore.Mvc;

namespace GestorFinanzasMVC.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View(); // Buscará la vista en Views/Account/Login.cshtml
        }

        public IActionResult Registro()
        {
            return View(); // Buscará la vista en Views/Account/Register.cshtml
        }

        public IActionResult RecuperarPassword()
        {
            return View(); // Buscará la vista en Views/Account/RecuperarPassword.cshtml
        }

        public IActionResult ResetearPassword(string token)
        {
            ViewBag.Token = token;
            return View(); // Buscará la vista en Views/Account/ResetearPassword.cshtml
        }
    }
}
