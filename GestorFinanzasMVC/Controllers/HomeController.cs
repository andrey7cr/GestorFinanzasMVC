using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestorFinanzasMVC.Models;

namespace GestorFinanzasMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        ViewBag.NombreUsuario = User.Identity.Name;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    //Para que le envíe datos luego del login
    public IActionResult Welcome()
    {
        ViewBag.NombreUsuario = User.Identity.Name;
        return View();
    }

}
