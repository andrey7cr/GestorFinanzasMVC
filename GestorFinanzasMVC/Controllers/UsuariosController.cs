using GestorFinanzasMVC.Models;
using GestorFinanzasMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;


namespace GestorFinanzasMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly GestorFinanzasDbContext _context;

        private readonly EmailService _emailService;

        public UsuariosController(GestorFinanzasDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Modelo no válido"); 
                return BadRequest(ModelState);
            }

            if (await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email))
            {
                Console.WriteLine("Correo ya registrado");
                return BadRequest(new { message = "El correo ya está registrado." });
            }

            _context.Usuarios.Add(usuario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Usuario registrado con éxito");
                return StatusCode(500, new { message = "Error interno al guardar el usuario.", error = ex.Message });
            }

            return Ok(new { message = "Usuario registrado con éxito." });
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == loginData.Email && u.Password == loginData.Password);

            if (usuario == null)
                return Unauthorized(new { message = "Credenciales inválidas." });

            // Crear claims
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, usuario.UsuarioId.ToString()),
        new Claim(ClaimTypes.Name, usuario.Nombre),
        new Claim(ClaimTypes.Email, usuario.Email)
    };

            var identity = new ClaimsIdentity(claims, "MiCookieAuth");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("MiCookieAuth", principal);

            return Ok(new { message = "Login con cookie exitoso" });
        }

        [HttpPost("RecuperarPassword")]
        public async Task<IActionResult> RecuperarPassword([FromBody] RecuperarPasswordDto recuperarPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == recuperarPasswordDto.Email);
            if (usuario == null)
            {
                return NotFound(new { message = "El correo no está registrado." });
            }

            var token = Guid.NewGuid().ToString();
            usuario.TokenRecuperacion = token;
            usuario.FechaExpiracionToken = DateTime.Now.AddHours(1);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno al generar el token de recuperación.", error = ex.Message });
            }

            var enlaceRecuperacion = Url.Action("ResetearPassword", "Account", new { token = token }, Request.Scheme);
            var mensajeCorreo = $"Para resetear tu contraseña, haz clic en el siguiente enlace: {enlaceRecuperacion}";

            await _emailService.SendEmailAsync(usuario.Email, "Recuperación de Contraseña", mensajeCorreo);

            return Ok(new { message = "Se ha enviado un correo con las instrucciones para recuperar tu contraseña." });
        }

        [HttpPost("ResetearPassword")]
        public async Task<IActionResult> ResetearPassword([FromBody] ResetearPasswordDto resetearPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.TokenRecuperacion == resetearPasswordDto.Token && u.FechaExpiracionToken > DateTime.Now);
            if (usuario == null)
            {
                return BadRequest(new { message = "El token es inválido o ha expirado." });
            }

            if (resetearPasswordDto.Password != resetearPasswordDto.ConfirmPassword)
            {
                return BadRequest(new { message = "Las contraseñas no coinciden." });
            }

            usuario.Password = resetearPasswordDto.Password;
            usuario.TokenRecuperacion = null;
            usuario.FechaExpiracionToken = null;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno al resetear la contraseña.", error = ex.Message });
            }

            return Ok(new { message = "Contraseña reseteada con éxito." });
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("MiCookieAuth");
            return Ok(new { message = "Sesión cerrada." });
        }

    }
}
