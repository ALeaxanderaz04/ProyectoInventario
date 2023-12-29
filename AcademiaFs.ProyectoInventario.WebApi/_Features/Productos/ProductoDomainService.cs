using AcademiaFs.ProyectoInventario.WebApi._Common;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using Farsiman.Application.Core.Standard.DTOs;

namespace AcademiaFs.ProyectoInventario.WebApi._Features.Productos
{
    public class ProductoDomainService
    {
        public Respuesta<bool> ProductoExiste(List<Producto> productos, int IdProducto)
        {
            var ProductoExiste = productos.FirstOrDefault(e => e.IdProducto == IdProducto);
            if (ProductoExiste == null)
                return Respuesta<bool>.Fault(MensajesCamposInvalidos.IdNoEncontrado("producto"));

            return Respuesta.Success(true);
        }
    }
}
