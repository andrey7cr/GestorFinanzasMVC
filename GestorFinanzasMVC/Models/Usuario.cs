using System;
using System.ComponentModel.DataAnnotations;

namespace GestorFinanzasMVC.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100)]
        public string Password { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}
