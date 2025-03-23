using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GestorFinanzasMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GestorFinanzasMVC.Controllers;

public class HomeController : Controller
{
    private readonly GestorFinanzasDbContext _context;
    private readonly ILogger<HomeController> _logger;

    public HomeController(GestorFinanzasDbContext context, ILogger<HomeController> logger)
    {
        _context = context;
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

    [Authorize]
    public async Task<IActionResult> Welcome()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var ingresos = await _context.Ingresos
            .Where(i => i.UsuarioId == userId)
            .SumAsync(i => i.Monto);
        var gastos = await _context.Gastos
            .Where(g => g.UsuarioId == userId)
            .SumAsync(g => g.Monto);

        var vm = new WelcomeVm
        {
            NombreUsuario = User.Identity.Name,
            TotalIngresos = ingresos,
            TotalGastos = gastos
        };

        return View(vm);
    }


}
