using AcademiaFs.ProyectoInventario.WebApi._Features.Lotes.Dto;
using AcademiaFs.ProyectoInventario.WebApi._Features.Sucursales.Dto;
using Farsiman.Application.Core.Standard.DTOs;

namespace AcademiaFs.ProyectoInventario.WebApi._Features.Lotes
{
    public interface ILoteAppServicecs
    {
        Respuesta<List<LoteListadoDto>> ListadoLotes();
        Respuesta<LoteDto> InsertarLote(LoteDto lote);
        Respuesta<LoteDto> EditarLote(LoteDto lote);
        Respuesta<LoteDto> EliminarLote(LoteDto lote);
    }
}
