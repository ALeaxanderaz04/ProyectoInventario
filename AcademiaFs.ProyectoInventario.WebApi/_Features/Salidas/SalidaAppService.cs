using AcademiaFs.ProyectoInventario.WebApi._Common;
using AcademiaFs.ProyectoInventario.WebApi._Features.Productos;
using AcademiaFs.ProyectoInventario.WebApi._Features.Salidas.Dto;
using AcademiaFs.ProyectoInventario.WebApi._Features.Sucursales.Dto;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using AcademiaFS.ProyectoTransporte._Common;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
using System.Security.Policy;

namespace AcademiaFs.ProyectoInventario.WebApi._Features.Salidas
{
    public class SalidaAppService : ISalidaAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        private readonly SalidaDomainService _salidaDomain;

        public SalidaAppService(IMapper mapper,
                                UnitOfworkBuilder unitOfWorkBuilder,
                                SalidaDomainService salidaDomain)
        {
            _mapper             = mapper;
            _unitOfWork         = unitOfWorkBuilder.BuilderDB01();
            _salidaDomain       = salidaDomain;
        }

        public Respuesta<SalidaDto> EditarSalida(SalidaDto salida)
        {
            throw new NotImplementedException();
        }

        public Respuesta<SalidaDto> EliminarSalida(SalidaDto salida)
        {
            throw new NotImplementedException();
        }

        public Respuesta<SalidaDto> InsertarSalida(SalidaDto salida)
        {

            salida.IdSalida = (int)ValoresDefecto.IdIngreso;
            var sucursales = _unitOfWork.Repository<Sucursal>().AsQueryable().Where(e => e.Activo).ToList();
            var salidas = ListadoSalidasSinRecibirEnSucursal(salida.IdSucursal);

            Respuesta<bool> esSalidaValida = _salidaDomain.EsSalidaValidaInsertar(sucursales, salida.IdSucursal, salidas);
            if (!esSalidaValida.Ok)
                return Respuesta.Fault<SalidaDto>(esSalidaValida.Mensaje, CodigosGlobales.ADVERTENCIA);

            //_unitOfWork.BeginTransaction();

            try
            {
                var salidaMepeada = _mapper.Map<Salida>(salida);

                salidaMepeada.Total                 = 0;
                salidaMepeada.IdEstadoEnvio         = (int)ValoresDefecto.IdEstadoEnviadoSucursal;
                salidaMepeada.IdUsuarioRecibe       = null;
                salidaMepeada.FechaRecibido         = null;
                salidaMepeada.IdUsuarioCreacion     = 1;
                salidaMepeada.FechaCreacion         = DateTime.Now;
                salidaMepeada.IdUsuarioModificacion = null;
                salidaMepeada.FechaModificacion     = null;
                salidaMepeada.Activo                = true;

                _unitOfWork.Repository<Salida>().Add(salidaMepeada);
                //_unitOfWork.Commit();
                _unitOfWork.SaveChanges();
                

                salida.IdSalida = salidaMepeada.IdSalida;

                Respuesta<bool> InsertarDetalle = InsertarDetalles(salida);

                if(InsertarDetalle.Ok)
                    return Respuesta.Success(salida, MensajesAcciones.EXITO_INSERTAR, CodigosGlobales.EXITO);
            }
            catch (Exception e)
            {
                //_unitOfWork.RollBack();
                throw;
            }

            throw new NotImplementedException();
        }

        public Respuesta<List<SalidaListadoDto>> ListadoSalidas()
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
                             where(salida.Activo)
                             select new SalidaListadoDto
                             {
                                 IdSalida                   = salida.IdSalida,
                                 IdSucursal                 = salida.IdSucursal,
                                 NombreSucursal             = sucursal.Nombre,
                                 Total                      = salida.Total,
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
                                 salidaDetalleListado       = (from salidetalle in _unitOfWork.Repository<SalidaDetalle>().AsQueryable()
                                                               join lote in _unitOfWork.Repository<Lote>().AsQueryable() 
                                                               on salidetalle.IdLote equals lote.IdLote
                                                               join producto in _unitOfWork.Repository<Producto>().AsQueryable()
                                                               on lote.IdProducto equals producto.IdProducto
                                                               where salidetalle.IdSalida == salida.IdSalida
                                                               select new SalidaDetalleListadoDto 
                                                               {
                                                                   IdSalidaDetalle  = salidetalle.IdSalidaDetalle,
                                                                   IdSalida         = salida.IdSalida,
                                                                   IdLote           = salidetalle.IdLote,
                                                                   NombreProducto   = producto.Nombre,
                                                                   CantidadProducto = salidetalle.CantidadProducto

                                                               }).ToList()
                             }).ToList();
            return Respuesta.Success(respuesta, "", CodigosGlobales.EXITO);
        }

        public List<Salida> ListadoSalidasSinRecibirEnSucursal(int IdSucursal)
        {
            var resultado = (from salida in _unitOfWork.Repository<Salida>().AsQueryable()
                             join sucursal in _unitOfWork.Repository<Sucursal>().AsQueryable()
                             on salida.IdSucursal equals sucursal.IdSucursal
                             where (salida.Activo) && (salida.IdEstadoEnvio == (int)ValoresDefecto.IdEstadoEnviadoSucursal)
                             && (salida.IdSucursal == IdSucursal)
                             select new Salida
                             {
                                 IdSalida =  salida.IdSalida,
                                 IdSucursal = salida.IdSucursal,
                                 Total = salida.Total,
                                 IdEstadoEnvio = salida.IdEstadoEnvio

                             }).ToList();

            return resultado;
        }

        public List<Lote> ListadoLotesProductoValidos(int IdProducto)
        {
            var resultado = (from lote in _unitOfWork.Repository<Lote>().AsQueryable()
                             where (lote.Activo) && (lote.CantidadActual > 0) && (lote.IdProducto == IdProducto)
                             orderby lote.FechaVencimiento ascending
                             select new Lote 
                             { 
                                IdLote = lote.IdLote,
                                IdProducto = lote.IdProducto,
                                CantidadIngresada = lote.CantidadIngresada,
                                CostoUnidad = lote.CostoUnidad,
                                FechaVencimiento = lote.FechaVencimiento,
                                CantidadActual = lote.CantidadActual
                             }).ToList();

            return resultado;
        }

        public Respuesta<bool> InsertarDetalles(SalidaDto salida)
        {
            
            foreach (var detalle in salida.SalidaDetalle ?? new List<SalidaDetalleDto>())
            {
                int cantidadProductoReducida = detalle.CantidadProducto;
                var lotesProducto = ListadoLotesProductoValidos(detalle.IdProducto);

                foreach (var item in lotesProducto)
                {
                    detalle.IdSalidaDetalle = (int)ValoresDefecto.IdIngreso;

                    SalidaDetalle detalleMapeado = new SalidaDetalle();
                       

                    detalleMapeado.IdSalida                 = salida.IdSalida;
                    detalleMapeado.IdUsuarioCreacion        = 1;
                    detalleMapeado.FechaCreacion            = DateTime.Now;
                    detalleMapeado.IdUsuarioModificacion    = null;
                    detalleMapeado.FechaModificacion        = null;
                    detalleMapeado.CantidadProducto=detalle.CantidadProducto;
                    detalleMapeado.Activo                   = true;

                    if (item.CantidadActual - detalle.CantidadProducto < 0)
                    {
                        detalle.CantidadProducto -= item.CantidadActual;
                        var EditarLote = _unitOfWork.Repository<Lote>().FirstOrDefault(e => e.IdLote == item.IdLote);

                        cantidadProductoReducida -= EditarLote.CantidadActual;
                        EditarLote.CantidadActual = 0;
                        _unitOfWork.Repository<Lote>().Update(EditarLote);

                        detalleMapeado.IdLote =EditarLote.IdLote;
                        _unitOfWork.Repository<SalidaDetalle>().Add(detalleMapeado);

                      
                    }
                    else
                    {
                        var EditarLote = _unitOfWork.Repository<Lote>().FirstOrDefault(e => e.IdLote == item.IdLote);

                        EditarLote.CantidadActual -= cantidadProductoReducida;
                        _unitOfWork.Repository<Lote>().Update(EditarLote);

                        detalleMapeado.IdLote = EditarLote.IdLote;

                        _unitOfWork.Repository<SalidaDetalle>().Add(detalleMapeado);
                        break;
                    }
                }
            }

            _unitOfWork.SaveChanges();

            return Respuesta.Success(true);
        }
    }
}
