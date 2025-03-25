
using GestorFinanzasMVC.Models;
using System.ComponentModel.DataAnnotations;

public class MetaFinanciera
{
    public int MetaFinancieraId { get; set; }

    [Required]
    public int UsuarioId { get; set; }

    [Required]
    public int CategoriaId { get; set; }

    [Required]
    public decimal MontoObjetivo { get; set; }

    [Required]
    public TipoMeta Tipo { get; set; } //ingreso o gasto

    public bool Alcanzada { get; set; } = false;

    public DateTime FechaRegistro { get; set; } = DateTime.Now;

    public Usuario Usuario { get; set; }
    public Categoria Categoria { get; set; }
}

public enum TipoMeta
{
    Ingreso,
    Gasto
}
