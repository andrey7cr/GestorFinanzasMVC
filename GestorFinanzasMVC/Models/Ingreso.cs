using System;
using System.ComponentModel.DataAnnotations;

namespace GestorFinanzasMVC.Models
{
    public class Ingreso
    {
        public int IngresoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Descripcion { get; set; }

        [Required]
        public decimal Monto { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Now;

        [Required]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        
        public int? UsuarioId { get; set; } 
        public Usuario? Usuario { get; set; } 
    }
}