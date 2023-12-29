namespace AcademiaFs.ProyectoInventario.WebApi._Features.Sucursales.Dto
{
    public class SucursalListadoDto
    {
        public int IdSucursal { get; set; }
        public string Nombre { get; set; } = null!;
        public string IdMunicipio { get; set; } = null!;
        public string NombreMunicipio { get; set; } = null!;
        public string IdDepartamento { get; set; } = null!;
        public string NombreDepartamento { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public int IdUsuarioCreacion { get; set; }
        public string NombreUsuarioCreacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public string? NombreUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool? Activo { get; set; }
    }
}
