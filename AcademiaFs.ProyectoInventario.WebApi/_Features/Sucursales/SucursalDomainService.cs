using AcademiaFs.ProyectoInventario.WebApi._Common;
using AcademiaFs.ProyectoInventario.WebApi._Features.Municipios;
using AcademiaFs.ProyectoInventario.WebApi._Features.Sucursales.Dto;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using Farsiman.Application.Core.Standard.DTOs;

namespace AcademiaFs.ProyectoInventario.WebApi._Features.Sucursales
{
    public class SucursalDomainService
    {
        private readonly MunicipioDomainService _municipioDomain;

        public SucursalDomainService(MunicipioDomainService municipioDomain)
        {
            _municipioDomain = municipioDomain;
        }

        public Respuesta<bool> EsSucursalValidaInsertar(SucursalDto sucursal, List<Municipio> municipios)
        {
            Respuesta<bool> esMunicipioValido = _municipioDomain.MunicipioExiste(municipios, sucursal.IdMunicipio);
            return esMunicipioValido;
        }

        public Respuesta<bool> EsSucursalValidaEditar(SucursalDto sucursal, List<Municipio> municipios, List<Sucursal> sucursales)
        {
            Respuesta<bool> sucursalExiste = SucursalExiste(sucursales, sucursal.IdSucursal);
            if (!sucursalExiste.Ok)
                return sucursalExiste;

            Respuesta<bool> municipioExiste = _municipioDomain.MunicipioExiste(municipios, sucursal.IdMunicipio);
            if (!municipioExiste.Ok)
                return municipioExiste;
            
            return Respuesta.Success(true);
        }

        public Respuesta<bool> SucursalExiste(List<Sucursal> sucursales, int IdSucursal)
        {
            var sucursalExiste = sucursales.FirstOrDefault(e => e.IdSucursal == IdSucursal);
            if (sucursalExiste == null)
            {
                return Respuesta<bool>.Fault(MensajesCamposInvalidos.IdNoEncontrado("sucursal"));
            }
            return Respuesta.Success(true);
        }
    }
}
