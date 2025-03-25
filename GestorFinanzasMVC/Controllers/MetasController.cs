using GestorFinanzasMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace GestorFinanzasMVC.Controllers
{
    [Authorize]
    public class MetasController : Controller
    {
        private readonly GestorFinanzasDbContext _context;

        public MetasController(GestorFinanzasDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Listar()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var metas = await _context.MetasFinancieras
                .Include(m => m.Categoria)
                .Where(m => m.UsuarioId == userId)
                .ToListAsync();

            return View(metas);
        }

        [HttpGet]
        public async Task<IActionResult> Crear()
        {
            ViewBag.Categorias = await _context.Categorias.ToListAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(MetaFinanciera meta)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            meta.UsuarioId = userId;
            meta.FechaRegistro = DateTime.Now;
            ModelState.Remove(nameof(meta.Usuario));
            ModelState.Remove(nameof(meta.Categoria));

            if (ModelState.IsValid)
            {
                _context.MetasFinancieras.Add(meta);
                Console.WriteLine($"Guardando meta: UsuarioId={meta.UsuarioId}, CategoriaId={meta.CategoriaId}, Monto={meta.MontoObjetivo}");

                await _context.SaveChangesAsync();
                return RedirectToAction("Listar");
            }

            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }


            // Si el modelo no es válido, recargamos las categorías
            ViewBag.Categorias = await _context.Categorias.ToListAsync();
            return View(meta);
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var meta = await _context.MetasFinancieras.FindAsync(id);
            if (meta != null)
            {
                _context.MetasFinancieras.Remove(meta);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Listar");
        }
    }
}
