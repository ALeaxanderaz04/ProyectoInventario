using AcademiaFs.ProyectoInventario.WebApi._Features.Lotes;
using AcademiaFs.ProyectoInventario.WebApi._Features.Lotes.Dto;
using AcademiaFs.ProyectoInventario.WebApi._Features.Usuarios;
using AcademiaFs.ProyectoInventario.WebApi._Features.Usuarios.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFs.ProyectoInventario.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoteController : ControllerBase
    {
        public readonly LoteAppService _sevice;
        public LoteController(LoteAppService lote)
        {
            _sevice = lote;
        }

        [HttpGet("ListadoLotes")]
        public IActionResult ListadoLotes()
        {
            var resultado = _sevice.ListadoLotes();
            return Ok(resultado);
        }

        [HttpPost("InsertarLote")]
        public IActionResult InsertarLote(LoteDto item)
        {
            var resultado = _sevice.InsertarLote(item);
            return Ok(resultado);
        }

        [HttpPut("EditarLote")]
        public IActionResult EditarLote(LoteDto item)
        {
            var resultado = _sevice.EditarLote(item);
            return Ok(resultado);
        }

        [HttpPut("EliminarLote")]
        public IActionResult EliminarLote(LoteDto item)
        {
            var resultado = _sevice.EliminarLote(item);
            return Ok(resultado);
        }
    }
}
