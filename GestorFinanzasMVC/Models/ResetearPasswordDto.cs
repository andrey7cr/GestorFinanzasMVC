using System.ComponentModel.DataAnnotations;

namespace GestorFinanzasMVC.Models
{
    public class ResetearPasswordDto
    {
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "La confirmación de contraseña es obligatoria.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Token { get; set; }
    }
}