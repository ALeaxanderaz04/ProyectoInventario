using System.ComponentModel.DataAnnotations;

namespace AcademiaFs.ProyectoInventario.WebApi._Features.Sucursales.Dto
{
    public class SucursalDto
    {
        public int IdSucursal { get; set; }
        [Required(ErrorMessage = "Nombre es requerido")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "Municipio es requerido")]
        public string IdMunicipio { get; set; } = null!;
        [Required(ErrorMessage = "Direccion es requerido")]
        public string Direccion { get; set; } = null!;
    }
}
