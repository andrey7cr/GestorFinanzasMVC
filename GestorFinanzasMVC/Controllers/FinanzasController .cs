using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using GestorFinanzasMVC.Models;

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
    }
}
