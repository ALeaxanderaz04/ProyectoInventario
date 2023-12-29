namespace AcademiaFs.ProyectoInventario.WebApi._Features.Salidas.Dto
{
    public class SalidaDetalleDto
    {
        public int IdSalidaDetalle { get; set; }
        public int IdSalida { get; set; }
        public int IdProducto { get; set; }
        public int CantidadProducto { get; set; }
    }
}
