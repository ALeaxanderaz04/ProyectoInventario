namespace AcademiaFs.ProyectoInventario.WebApi._Features.Usuarios.Dto
{
    public class UsuarioListadoDto
    {
        public int IdUsuario { get; set; }
        public string NombreUsuario { get; set; } = null!;
        public bool? EsAdmin { get; set; }
        public int? IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; } =  null!;
        public int? IdRol { get; set; }
        public string NombreRol { get; set; } = null!;
        public int IdUsuarioCreacion { get; set; }
        public string NombreUsuarioCreacion { get; set; } = null!;
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public string? NombreUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
