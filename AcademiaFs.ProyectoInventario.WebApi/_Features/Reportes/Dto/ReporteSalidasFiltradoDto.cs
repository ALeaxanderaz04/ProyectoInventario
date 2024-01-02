using AcademiaFs.ProyectoInventario.WebApi._Features.Salidas.Dto;

namespace AcademiaFs.ProyectoInventario.WebApi._Features.Reportes.Dto
{
    public class ReporteSalidasFiltradoDto
    {
        public int IdSalida { get; set; }
        public int IdSucursal { get; set; }
        public string NombreSucursal { get; set; } = null!;
        public DateTime FechaSalida { get; set; }
        public int UnidadesTotales { get; set; }
        public decimal Total { get; set; }
        public int IdEstadoEnvio { get; set; }
        public string NombreEstadoEnvio { get; set; } = null!;
        public DateTime? FechaRecibido { get; set; }
        public int? IdUsuarioRecibe { get; set; }
        public string? NombreUsuarioRecibe { get; set; }
        public int IdUsuarioCreacion { get; set; }
        public string NombreUsuarioCreacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public string NombreUsuarioModificacion { get; set; } = null!;
        public DateTime? FechaModificacion { get; set; }
        public bool? Activo { get; set; }
    }
}
