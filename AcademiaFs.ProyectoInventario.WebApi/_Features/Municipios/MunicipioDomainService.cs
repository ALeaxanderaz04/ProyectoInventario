using AcademiaFs.ProyectoInventario.WebApi._Common;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using Farsiman.Application.Core.Standard.DTOs;

namespace AcademiaFs.ProyectoInventario.WebApi._Features.Municipios
{
    public class MunicipioDomainService
    {
        public Respuesta<bool> MunicipioExiste(List<Municipio> municipios, string IdMuniciopio)
        {
            var municicioExiste = municipios.FirstOrDefault(e => e.IdMunicipio == IdMuniciopio);

            if (municicioExiste == null)
                return Respuesta<bool>.Fault(MensajesCamposInvalidos.IdNoEncontrado("municipio"));
            
            return Respuesta.Success(true);
        }
    }
}
