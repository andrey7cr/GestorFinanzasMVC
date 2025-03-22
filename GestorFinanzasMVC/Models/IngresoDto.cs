namespace GestorFinanzasMVC.Models
{
    public class IngresoDto
    {
        public string Descripcion { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public int CategoriaId { get; set; }
    }

}
