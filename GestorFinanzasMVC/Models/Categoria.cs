using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GestorFinanzasMVC.Models
{
    public class Categoria
    {
        [Key] 
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100)]
        public string Nombre { get; set; }

        // Relaciones
        public ICollection<Ingreso>? Ingresos { get; set; }
        public ICollection<Gasto>? Gastos { get; set; }
    }
}