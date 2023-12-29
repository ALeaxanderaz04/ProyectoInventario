namespace AcademiaFs.ProyectoInventario.WebApi._Features.Salidas.Dto
{
    public class SalidaDto
    {
        public int IdSalida { get; set; }
        public int IdSucursal { get; set; }
        public DateTime FechaSalida { get; set; }
        public List<SalidaDetalleDto>? SalidaDetalle { get; set; }
    }
}
