using AcademiaFs.ProyectoInventario.WebApi._Features.Login;
using AcademiaFs.ProyectoInventario.WebApi._Features.Usuarios.Dto;
using AcademiaFS.ProyectoTransporte._Features.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFS.ProyectoTransporte.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public readonly LoginService _sevice;
        public LoginController(LoginService login)
        {
            _sevice = login;
        }

        [HttpPut("IniciarSesion")]
        public IActionResult IniciarSesion(UsuarioDto usuario)
        {
            var resultado = _sevice.InicarSesion(usuario);
            return Ok(resultado);
        }
    }
}
