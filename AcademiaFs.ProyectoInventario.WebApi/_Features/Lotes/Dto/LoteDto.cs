namespace AcademiaFs.ProyectoInventario.WebApi._Features.Lotes.Dto
{
    public class LoteDto
    {
        public int IdLote { get; set; }
        public int IdProducto { get; set; }
        public int CantidadIngresada { get; set; }
        public decimal CostoUnidad { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
