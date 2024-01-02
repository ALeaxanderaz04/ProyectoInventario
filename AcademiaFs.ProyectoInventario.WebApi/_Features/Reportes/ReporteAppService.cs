using AcademiaFs.ProyectoInventario.WebApi._Features.Reportes.Dto;
using AcademiaFs.ProyectoInventario.WebApi._Features.Salidas;
using AcademiaFs.ProyectoInventario.WebApi._Features.Salidas.Dto;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using AcademiaFS.ProyectoTransporte._Common;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;

namespace AcademiaFs.ProyectoInventario.WebApi._Features.Reportes
{
    public class ReporteAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;

        public ReporteAppService(IMapper mapper,
                                UnitOfworkBuilder unitOfWorkBuilder)
        {
            _mapper             = mapper;
            _unitOfWork         = unitOfWorkBuilder.BuilderDB01();
        }

        public Respuesta<List<ReporteSalidasFiltradoDto>> ListadoSalidasFiltrado(DateTime fechaIncio,DateTime fechaFin, int Idsucursal)
        {
            var respuesta = (from salida in _unitOfWork.Repository<Salida>().AsQueryable()
                             join sucursal in _unitOfWork.Repository<Sucursal>().AsQueryable()
                             on salida.IdSucursal equals sucursal.IdSucursal
                             join usuarioRecibe in _unitOfWork.Repository<Usuario>().AsQueryable()
                             on salida.IdUsuarioRecibe equals usuarioRecibe.IdUsuario into recibe
                             from usuarioRecibe in recibe.DefaultIfEmpty()
                             join estadoEnvio in _unitOfWork.Repository<EstadoEnvio>().AsQueryable()
                             on salida.IdEstadoEnvio equals estadoEnvio.IdEstadoEnvio
                             join usuarioCrea in _unitOfWork.Repository<Usuario>().AsQueryable()
                             on salida.IdUsuarioCreacion equals usuarioCrea.IdUsuario
                             join usuarioModifica in _unitOfWork.Repository<Usuario>().AsQueryable()
                             on salida.IdUsuarioModificacion equals usuarioModifica.IdUsuario into modificacion
                             from usuarioModifica in modificacion.DefaultIfEmpty()
                             where  (salida.Activo) && 
                                    (salida.FechaSalida.Date >= fechaIncio.Date && salida.FechaSalida.Date <= fechaFin.Date) &&
                                    (salida.IdSucursal == Idsucursal)
                             select new ReporteSalidasFiltradoDto
                             {
                                 IdSalida                   = salida.IdSalida,
                                 IdSucursal                 = salida.IdSucursal,
                                 NombreSucursal             = sucursal.Nombre,
                                 Total                      = salida.Total,
                                 UnidadesTotales            = (int)(from salidadetalle in _unitOfWork.Repository<SalidaDetalle>().AsQueryable()
                                                                             where salidadetalle.IdSalida == salida.IdSalida
                                                                             select salidadetalle.CantidadProducto).Sum(),
                                 FechaSalida                = salida.FechaSalida,
                                 IdEstadoEnvio              = salida.IdEstadoEnvio,
                                 NombreEstadoEnvio          = estadoEnvio.Nombre,
                                 IdUsuarioRecibe            = salida.IdUsuarioRecibe,
                                 NombreUsuarioRecibe        = usuarioRecibe.NombreUsuario,
                                 FechaRecibido              = salida.FechaRecibido,
                                 IdUsuarioCreacion          = salida.IdUsuarioCreacion,
                                 NombreUsuarioCreacion      = usuarioCrea.NombreUsuario,
                                 FechaCreacion              = salida.FechaCreacion,
                                 IdUsuarioModificacion      = salida.IdUsuarioModificacion,
                                 NombreUsuarioModificacion  = usuarioModifica.NombreUsuario,
                                 Activo                     = salida.Activo,
                                
                             }).ToList();

            return Respuesta.Success(respuesta, "", CodigosGlobales.EXITO);
        }

    }
}
