using AcademiaFs.ProyectoInventario.WebApi._Common;
using AcademiaFs.ProyectoInventario.WebApi._Features.Productos;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using Farsiman.Application.Core.Standard.DTOs;

namespace AcademiaFs.ProyectoInventario.WebApi._Features.Lotes
{
    public class LoteDomainService
    {
        private readonly ProductoDomainService _productoDomain;

        public LoteDomainService(ProductoDomainService productoDomain)
        {
            _productoDomain = productoDomain;
        }

        public Respuesta<bool> EsLoteValidoInsertar(List<Producto> productos, int IdProducto)
        {
            Respuesta<bool> productoExiste = _productoDomain.ProductoExiste(productos, IdProducto);
            return productoExiste;
        }

        public Respuesta<bool> EsloteValidoEditar(List<Producto> productos, int IdProducto, List<Lote> lotes, int IdLote)
        {
            Respuesta<bool> loteExiste = LoteExiste(lotes, IdLote);
            if (!loteExiste.Ok)
                return loteExiste;

            Respuesta<bool> productoExiste = _productoDomain.ProductoExiste(productos, IdProducto);
            if (!productoExiste.Ok)
                return productoExiste;

            return Respuesta.Success(true);
        }

        public Respuesta<bool> LoteExiste(List<Lote> lotes, int IdLote)
        {
            var loteExiste = lotes.FirstOrDefault(e => e.IdLote == IdLote);
            if (loteExiste == null)
            {
                return Respuesta<bool>.Fault(MensajesCamposInvalidos.IdNoEncontrado("lote"));
            }
            return Respuesta.Success(true);
        }
    }
}
