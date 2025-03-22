using System;
using System.ComponentModel.DataAnnotations;

namespace GestorFinanzasMVC.Models
{
    public class GastoDto
    {
        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; }

        [Required]
        public decimal Monto { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public int CategoriaId { get; set; }
    }
}
