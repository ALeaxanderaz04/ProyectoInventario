namespace AcademiaFs.ProyectoInventario.WebApi._Features.Lotes.Dto
{
    public class LoteListadoDto
    {
        public int IdLote { get; set; }
        public int IdProducto { get; set; }
        public string NombreProducto { get; set; } = null!;
        public int CantidadIngresada { get; set; }
        public decimal CostoUnidad { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int CantidadActual { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public string NombreUsuarioCreacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public string? NombreUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool? Activo { get; set; }
    }
}
