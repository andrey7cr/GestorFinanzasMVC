using Microsoft.AspNetCore.Mvc;

namespace GestorFinanzasMVC.Controllers
{
    public class FinanzasController : Controller
    {
        public IActionResult Registrar()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}