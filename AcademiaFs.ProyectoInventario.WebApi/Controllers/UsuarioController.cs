using AcademiaFs.ProyectoInventario.WebApi._Features.Usuarios;
using AcademiaFs.ProyectoInventario.WebApi._Features.Usuarios.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFs.ProyectoInventario.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        public readonly UsuarioService _sevice;
        public UsuarioController(UsuarioService usuario)
        {
            _sevice = usuario;
        }

        [HttpGet("ListadoUsuarios")]
        public IActionResult ListadoUsuarios()
        {
            var resultado = _sevice.ListadoUsuarios();
            return Ok(resultado);
        }

        [HttpPost("InsertarUsuario")]
        public IActionResult InsertarUsuario(UsuarioDto item)
        {
            var resultado = _sevice.InsertarUsuario(item);
            return Ok(resultado);
        }
    }
}
