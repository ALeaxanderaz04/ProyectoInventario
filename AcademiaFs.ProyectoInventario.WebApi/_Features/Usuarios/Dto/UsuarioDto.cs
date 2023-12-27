using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademiaFs.ProyectoInventario.WebApi._Features.Usuarios.Dto
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        [Required]
        public string NombreUsuario { get; set; } = null!;
        [Required]
        public string Contrasena { get; set; } = null!;
        public bool? EsAdmin { get; set; }
        public int? IdEmpleado { get; set; }
        public int? IdRol { get; set; }
    }
}
