using System.ComponentModel.DataAnnotations;

namespace AcademiaFs.ProyectoInventario.WebApi._Features.Sucursales.Dto
{
    public class SucursalDto
    {
        public int IdSucursal { get; set; }
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string IdMunicipio { get; set; } = null!;
        [Required]
        public string Direccion { get; set; } = null!;
    }
}
