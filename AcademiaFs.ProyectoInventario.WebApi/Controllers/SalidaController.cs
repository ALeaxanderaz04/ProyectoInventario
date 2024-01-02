using AcademiaFs.ProyectoInventario.WebApi._Features.Productos;
using AcademiaFs.ProyectoInventario.WebApi._Features.Reportes;
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
        public readonly ReporteAppService _reporteSevice;
        public SalidaController(SalidaAppService salida,
                                ReporteAppService reporteService)
        {
            _sevice = salida;
            _reporteSevice = reporteService;
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

        [HttpPut("RecibirSalida")]
        public IActionResult RecibirSalida(int IdSalida)
        {
            var resultado = _sevice.ActualizarEstadoSalidaRecibido(IdSalida);
            return Ok(resultado);
        }

        [HttpPut("ReporteSalidas")]
        public IActionResult ReporteListadoSalidas(DateTime fechaInicio, DateTime fechaFin, int IdSucursal)
        {
            var resultado = _reporteSevice.ListadoSalidasFiltrado(fechaInicio, fechaFin, IdSucursal);
            return Ok(resultado);
        }


    }
}
