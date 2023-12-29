using AcademiaFs.ProyectoInventario.WebApi._Features.Salidas.Dto;
using AcademiaFs.ProyectoInventario.WebApi._Features.Sucursales.Dto;
using Farsiman.Application.Core.Standard.DTOs;

namespace AcademiaFs.ProyectoInventario.WebApi._Features.Salidas
{
    public interface ISalidaAppService
    {
        Respuesta<List<SalidaListadoDto>> ListadoSalidas();
        Respuesta<SalidaDto> InsertarSalida(SalidaDto salida);
        Respuesta<SalidaDto> EditarSalida(SalidaDto salida);
        Respuesta<SalidaDto> EliminarSalida(SalidaDto salida);
        Respuesta<bool> InsertarDetalles(SalidaDto salida);
    }
}
