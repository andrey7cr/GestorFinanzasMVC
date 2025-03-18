using System.ComponentModel.DataAnnotations;

namespace GestorFinanzasMVC.Models
{
    public class RecuperarPasswordDto
    {
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del correo es inválido.")]
        public string Email { get; set; }
    }
}