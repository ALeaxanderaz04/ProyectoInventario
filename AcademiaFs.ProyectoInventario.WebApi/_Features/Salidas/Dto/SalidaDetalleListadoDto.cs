namespace AcademiaFs.ProyectoInventario.WebApi._Features.Salidas.Dto
{
    public class SalidaDetalleListadoDto
    {
        public int IdSalidaDetalle { get; set; }
        public int IdSalida { get; set; }
        public int IdLote { get; set; }
        public string NombreProducto { get; set; } = null!;
        public int CantidadProducto { get; set; }
    }
}
