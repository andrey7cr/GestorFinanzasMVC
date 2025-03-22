using GestorFinanzasMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GestorFinanzasMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GastosController : ControllerBase
    {
        private readonly GestorFinanzasDbContext _context;

        public GastosController(GestorFinanzasDbContext context)
        {
            _context = context;
        }

        // GET: api/Gastos
        [HttpGet]
        public async Task<IActionResult> GetGastos()
        {
            var gastos = await _context.Gastos.Include(g => g.Categoria).ToListAsync();
            return Ok(gastos);
        }

        // GET: api/Gastos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGasto(int id)
        {
            var gasto = await _context.Gastos.Include(g => g.Categoria).FirstOrDefaultAsync(g => g.GastoId == id);
            if (gasto == null)
            {
                return NotFound();
            }
            return Ok(gasto);
        }

        // POST: api/Gastos
        [HttpPost]
        public async Task<IActionResult> PostGasto([FromBody] GastoDto gastoDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (string.IsNullOrEmpty(usuarioIdClaim))
                {
                    return BadRequest("No se pudo obtener el ID del usuario.");
                }

                var gasto = new Gasto
                {
                    Descripcion = gastoDto.Descripcion,
                    Monto = gastoDto.Monto,
                    Fecha = gastoDto.Fecha,
                    CategoriaId = gastoDto.CategoriaId,
                    UsuarioId = int.Parse(usuarioIdClaim)
                };

                var categoria = await _context.Categorias.FindAsync(gasto.CategoriaId);
                if (categoria == null)
                {
                    return BadRequest("Categoría no encontrada.");
                }

                gasto.Categoria = categoria;

                _context.Gastos.Add(gasto);
                await _context.SaveChangesAsync();

                // ✅ Aquí devolvés una respuesta JSON simple sin objetos cíclicos
                return Ok(new { message = "Gasto registrado", gastoId = gasto.GastoId });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en PostGasto: " + ex.Message);
                return StatusCode(500, new { message = "Error interno al registrar el gasto.", error = ex.Message });
            }
        }




        // PUT: api/Gastos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGasto(int id, [FromBody] Gasto gasto)
        {
            if (id != gasto.GastoId)
            {
                return BadRequest();
            }

            _context.Entry(gasto).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Gastos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGasto(int id)
        {
            var gasto = await _context.Gastos.FindAsync(id);
            if (gasto == null)
            {
                return NotFound();
            }

            _context.Gastos.Remove(gasto);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}