using AcademiaFs.ProyectoInventario.WebApi._Features.Lotes;
using AcademiaFs.ProyectoInventario.WebApi._Features.Lotes.Dto;
using AcademiaFs.ProyectoInventario.WebApi._Features.Sucursales;
using AcademiaFs.ProyectoInventario.WebApi._Features.Sucursales.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AcademiaFs.ProyectoInventario.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalController : ControllerBase
    {
        public readonly SucursalAppService _sevice;
        public SucursalController(SucursalAppService sucursal)
        {
            _sevice = sucursal;
        }

        [HttpGet("ListadoSucursales")]
        public IActionResult ListadoSucursales()
        {
            var resultado = _sevice.ListadoSucursales();
            return Ok(resultado);
        }

        [HttpPost("InsertarSucursal")]
        public IActionResult InsertarSucursal(SucursalDto item)
        {
            var resultado = _sevice.InsertarSucursal(item);
            return Ok(resultado);
        }

        [HttpPut("EditarSucursal")]
        public IActionResult EditarSucursal(SucursalDto item)
        {
            var resultado = _sevice.EditarSucursal(item);
            return Ok(resultado);
        }

        [HttpPut("EliminarSucursal")]
        public IActionResult EliminarSucursal(SucursalDto item)
        {
            var resultado = _sevice.EliminarSucursal(item);
            return Ok(resultado);
        }
    }
}
