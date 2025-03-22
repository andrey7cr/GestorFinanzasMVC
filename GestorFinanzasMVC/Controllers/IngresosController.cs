using GestorFinanzasMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace GestorFinanzasMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresosController : ControllerBase
    {
        private readonly GestorFinanzasDbContext _context;

        public IngresosController(GestorFinanzasDbContext context)
        {
            _context = context;
        }

        // GET: api/Ingresos
        [HttpGet]
        public async Task<IActionResult> GetIngresos()
        {
            var ingresos = await _context.Ingresos.Include(i => i.Categoria).ToListAsync();
            return Ok(ingresos);
        }

        // GET: api/Ingresos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIngreso(int id)
        {
            var ingreso = await _context.Ingresos.Include(i => i.Categoria).FirstOrDefaultAsync(i => i.IngresoId == id);
            if (ingreso == null)
            {
                return NotFound();
            }
            return Ok(ingreso);
        }

        // POST: api/Ingresos
        [HttpPost]
        public async Task<IActionResult> PostIngreso([FromBody] IngresoDto ingresoDto)
        {
            var usuarioId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var ingreso = new Ingreso
            {
                Descripcion = ingresoDto.Descripcion,
                Monto = ingresoDto.Monto,
                Fecha = ingresoDto.Fecha,
                CategoriaId = ingresoDto.CategoriaId,
                UsuarioId = usuarioId
            };

            _context.Ingresos.Add(ingreso);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIngreso), new { id = ingreso.IngresoId }, ingreso);
        }
        // PUT: api/Ingresos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngreso(int id, [FromBody] Ingreso ingreso)
        {
            if (id != ingreso.IngresoId)
            {
                return BadRequest();
            }

            _context.Entry(ingreso).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Ingresos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngreso(int id)
        {
            var ingreso = await _context.Ingresos.FindAsync(id);
            if (ingreso == null)
            {
                return NotFound();
            }

            _context.Ingresos.Remove(ingreso);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}