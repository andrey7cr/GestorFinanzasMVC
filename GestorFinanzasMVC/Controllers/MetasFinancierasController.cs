using GestorFinanzasMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class MetasFinancierasController : ControllerBase
{
    private readonly GestorFinanzasDbContext _context;

    public MetasFinancierasController(GestorFinanzasDbContext context)
    {
        _context = context;
    }

    [HttpGet("{usuarioId}")]
    public async Task<IActionResult> GetMetas(int usuarioId)
    {
        var metas = await _context.MetasFinancieras
            .Include(m => m.Categoria)
            .Where(m => m.UsuarioId == usuarioId)
            .ToListAsync();
        return Ok(metas);
    }

    [HttpPost]
    public async Task<IActionResult> CrearMeta([FromBody] MetaFinanciera meta)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        _context.MetasFinancieras.Add(meta);
        await _context.SaveChangesAsync();
        return Ok(meta);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Eliminar(int id)
    {
        var meta = await _context.MetasFinancieras.FindAsync(id);
        if (meta == null) return NotFound();

        _context.MetasFinancieras.Remove(meta);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
