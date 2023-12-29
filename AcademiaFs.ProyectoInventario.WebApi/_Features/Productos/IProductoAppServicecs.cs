using AcademiaFs.ProyectoInventario.WebApi._Features.Productos.Dto;
using Farsiman.Application.Core.Standard.DTOs;

namespace AcademiaFs.ProyectoInventario.WebApi._Features.Productos
{
    public interface IProductoAppServicecs
    {
        Respuesta<List<ProductoListadoDto>> ListadoProductos();
        Respuesta<ProductoDto> InsertarProducto(ProductoDto producto);
        Respuesta<ProductoDto> EditarProducto(ProductoDto producto);
        Respuesta<ProductoDto> EliminarProducto(ProductoDto producto);
    }
}
