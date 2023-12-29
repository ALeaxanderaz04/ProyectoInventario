using System.ComponentModel.DataAnnotations;

namespace AcademiaFs.ProyectoInventario.WebApi._Features.Productos.Dto
{
    public class ProductoDto
    {
        public int IdProducto { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;
    }
}
