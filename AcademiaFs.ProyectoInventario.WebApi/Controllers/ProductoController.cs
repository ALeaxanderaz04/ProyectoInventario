using AcademiaFs.ProyectoInventario.WebApi._Features.Lotes.Dto;
using AcademiaFs.ProyectoInventario.WebApi._Features.Lotes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AcademiaFs.ProyectoInventario.WebApi._Features.Productos;
using AcademiaFs.ProyectoInventario.WebApi._Features.Productos.Dto;

namespace AcademiaFs.ProyectoInventario.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        public readonly ProductoAppService _sevice;
        public ProductoController(ProductoAppService producto)
        {
            _sevice = producto;
        }

        [HttpGet("ListadoProductos")]
        public IActionResult ListadoProducto()
        {
            var resultado = _sevice.ListadoProductos();
            return Ok(resultado);
        }

        [HttpPost("InsertarProducto")]
        public IActionResult InsertarProducto(ProductoDto item)
        {
            var resultado = _sevice.InsertarProducto(item);
            return Ok(resultado);
        }

        [HttpPut("EditarProducto")]
        public IActionResult EditarProducto(ProductoDto item)
        {
            var resultado = _sevice.EditarProducto(item);
            return Ok(resultado);
        }

        [HttpPut("EliminarProducto")]
        public IActionResult EliminarProducto(ProductoDto item)
        {
            var resultado = _sevice.EliminarProducto(item);
            return Ok(resultado);
        }
    }
}
