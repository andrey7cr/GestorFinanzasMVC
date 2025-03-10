using GestorFinanzasMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GestorFinanzasMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly GestorFinanzasDbContext _context;

        public UsuariosController(GestorFinanzasDbContext context)
        {
            _context = context;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email))
            {
                return BadRequest(new { message = "El correo ya está registrado." });
            }

            _context.Usuarios.Add(usuario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno al guardar el usuario.", error = ex.Message });
            }

            return Ok(new { message = "Usuario registrado con éxito." });
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuarioEncontrado = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == loginData.Email && u.Password == loginData.Password);

            if (usuarioEncontrado == null)
            {
                return Unauthorized(new { message = "Credenciales inválidas." });
            }

            return Ok(new { message = "Login exitoso.", usuarioId = usuarioEncontrado.UsuarioId });
        }


    }
}
