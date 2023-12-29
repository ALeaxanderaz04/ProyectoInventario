using AcademiaFs.ProyectoInventario.WebApi._Features.Sucursales.Dto;
using Farsiman.Application.Core.Standard.DTOs;

namespace AcademiaFs.ProyectoInventario.WebApi._Features.Sucursales
{
    public interface ISucursalAppService
    {
        Respuesta<List<SucursalListadoDto>> ListadoSucursales();
        Respuesta<SucursalDto> InsertarSucursal(SucursalDto sucursal);
        Respuesta<SucursalDto> EditarSucursal(SucursalDto sucursal);
        Respuesta<SucursalDto> EliminarSucursal(SucursalDto sucursal);
    }
}
