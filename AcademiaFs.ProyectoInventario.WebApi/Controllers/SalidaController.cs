using AcademiaFs.ProyectoInventario.WebApi._Features.Productos;
using AcademiaFs.ProyectoInventario.WebApi._Features.Salidas;
using AcademiaFs.ProyectoInventario.WebApi._Features.Salidas.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFs.ProyectoInventario.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalidaController : ControllerBase
    {
        public readonly SalidaAppService _sevice;
        public SalidaController(SalidaAppService salida)
        {
            _sevice = salida;
        }

        [HttpGet("ListadoSalidas")]
        public IActionResult ListadoSalidas()
        {
            var resultado = _sevice.ListadoSalidas();
            return Ok(resultado);
        }

        [HttpPost("InsertarSalida")]
        public IActionResult InsertarSalida(SalidaDto item)
        {
            var resultado = _sevice.InsertarSalida(item);
            return Ok(resultado);
        }

    }
}
