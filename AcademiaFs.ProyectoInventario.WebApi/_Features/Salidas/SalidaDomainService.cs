using AcademiaFs.ProyectoInventario.WebApi._Common;
using AcademiaFs.ProyectoInventario.WebApi._Features.Lotes.Dto;
using AcademiaFs.ProyectoInventario.WebApi._Features.Sucursales;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using Farsiman.Application.Core.Standard.DTOs;

namespace AcademiaFs.ProyectoInventario.WebApi._Features.Salidas
{
    public class SalidaDomainService
    {
        private readonly SucursalDomainService _sucursalDomain;

        public SalidaDomainService(SucursalDomainService sucursalDomain) 
        { 
            _sucursalDomain = sucursalDomain;
        }
        public Respuesta<bool> EsSalidaValidaInsertar(List<Sucursal> sucursales, int IdSucursal, List<Salida> salidas)
        {
            Respuesta<bool> sucursalExiste = _sucursalDomain.SucursalExiste(sucursales, IdSucursal);
            if (!sucursalExiste.Ok)
                return sucursalExiste;

            decimal sumaCantidadCostoProducto = SumarCantidadCostoPedidosSucusal(salidas);
            if (sumaCantidadCostoProducto > (int)ValoresDefecto.MaximaCantidadCostoSalidas)
                return Respuesta.Fault<bool>(MensajesAcciones.SUCURSAL_EXCEDE_COSTO_ENVIO);

            return Respuesta.Success(true);
        }

        public decimal SumarCantidadCostoPedidosSucusal(List<Salida> salidas)
        {
            decimal sumaCantidadCostoProducto = salidas.Sum(e => e.Total);
            return sumaCantidadCostoProducto;
        }


        public List<Lote> lotesIngresar(List<Lote> lotes, int cantidad)
        {
            List<Lote> lotesValidos = new List<Lote>();
            foreach (var item in lotes)
            {
                if (item.CantidadActual - cantidad < 0)
                {
                    cantidad -= item.CantidadActual;
                    lotesValidos.Add(item);
                }
                else
                {
                    lotesValidos.Add(item);
                    break;
                }
            }

            return lotesValidos;
        }
    }
}
