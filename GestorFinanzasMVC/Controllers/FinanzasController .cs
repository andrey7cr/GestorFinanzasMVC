using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using GestorFinanzasMVC.Models;
using System.Text.Json;

namespace GestorFinanzasMVC.Controllers
{
    public class FinanzasController : Controller
    {
        private readonly GestorFinanzasDbContext _context;

        public FinanzasController(GestorFinanzasDbContext context)
        {
            _context = context;
        }

        public IActionResult Registrar()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Resumen()
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var ingresos = await _context.Ingresos
                .Where(i => i.UsuarioId == usuarioId)
                .Include(i => i.Categoria)
                .OrderByDescending(i => i.Fecha)
                .ToListAsync();

            var gastos = await _context.Gastos
                .Where(g => g.UsuarioId == usuarioId)
                .Include(g => g.Categoria)
                .OrderByDescending(g => g.Fecha)
                .ToListAsync();

            return View("Resumen", ((IEnumerable<Ingreso>)ingresos, (IEnumerable<Gasto>)gastos));
        }

        [Authorize]
        public async Task<IActionResult> Grafico()
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var ingresos = await _context.Ingresos
                .Where(i => i.UsuarioId == usuarioId)
                .ToListAsync();

            var gastos = await _context.Gastos
                .Where(g => g.UsuarioId == usuarioId)
                .ToListAsync();

            var ingresosPorCategoria = await _context.Ingresos
                .Where(i => i.UsuarioId == usuarioId)
                .Include(i => i.Categoria)
                .GroupBy(i => i.Categoria.Nombre)
                .Select(g => new { Categoria = g.Key, Total = g.Sum(i => i.Monto) })
                .ToListAsync();

            var gastosPorCategoria = await _context.Gastos
                .Where(g => g.UsuarioId == usuarioId)
                .Include(g => g.Categoria)
                .GroupBy(g => g.Categoria.Nombre)
                .Select(g => new { Categoria = g.Key, Total = g.Sum(g => g.Monto) })
                .ToListAsync();


            ViewBag.TotalIngresos = ingresos.Sum(i => i.Monto);
            ViewBag.TotalGastos = gastos.Sum(g => g.Monto);
            ViewBag.IngresosPorCategoriaJson = JsonSerializer.Serialize(ingresosPorCategoria);
            ViewBag.GastosPorCategoriaJson = JsonSerializer.Serialize(gastosPorCategoria);

            Console.WriteLine("Ingresos por categoría:");
            foreach (var i in ingresosPorCategoria)
            {
                Console.WriteLine($"{i.Categoria}: {i.Total}");
            }

            return View();
        }
    }
}
