
namespace AcademiaFs.ProyectoInventario.WebApi._Features.Productos.Dto
{
    public class ProductoListadoDto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; } = null!;
        public int IdUsuarioCreacion { get; set; }
        public string NombreUsuarioCreacion { get; set;} = null!;
        public DateTime FechaCreacion { get; set; }
        public int? IdUsuarioModificacion { get; set; }
        public string? NombreUsuarioModificacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public bool? Activo { get; set; }
    }
}
