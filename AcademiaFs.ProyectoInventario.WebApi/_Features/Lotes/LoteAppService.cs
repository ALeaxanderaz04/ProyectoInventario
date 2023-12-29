using AcademiaFs.ProyectoInventario.WebApi._Common;
using AcademiaFs.ProyectoInventario.WebApi._Features.Lotes.Dto;
using AcademiaFs.ProyectoInventario.WebApi._Features.Productos.Dto;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using AcademiaFS.ProyectoTransporte._Common;
using AcademiaFS.ProyectoTransporte._Features.Login;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;

namespace AcademiaFs.ProyectoInventario.WebApi._Features.Lotes
{
    public class LoteAppService : ILoteAppServicecs
    {
        private readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        private readonly LoteDomainService _loteDomainService;

        public LoteAppService(IMapper mapper,
                            UnitOfworkBuilder unitOfWorkBuilder,
                            LoteDomainService loteDomainService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWorkBuilder.BuilderDB01();
            _loteDomainService=loteDomainService;
        }

        public Respuesta<List<LoteListadoDto>> ListadoLotes()
        {
            var resultado = (from lote in _unitOfWork.Repository<Lote>().AsQueryable()
                             join producto in _unitOfWork.Repository<Producto>().AsQueryable()
                             on lote.IdProducto equals producto.IdProducto
                             join usuarioCrea in _unitOfWork.Repository<Usuario>().AsQueryable()
                             on lote.IdUsuarioCreacion equals usuarioCrea.IdUsuario
                             join usuarioModifica in _unitOfWork.Repository<Usuario>().AsQueryable()
                             on lote.IdUsuarioModificacion equals usuarioModifica.IdUsuario into modificacion
                             from usuarioModifica in modificacion.DefaultIfEmpty()
                             where (lote.Activo)
                             select new LoteListadoDto
                             {
                                 IdLote                      = lote.IdLote,
                                 IdProducto                  = lote.IdProducto,
                                 NombreProducto              = producto.Nombre,
                                 CantidadIngresada           = lote.CantidadIngresada,
                                 CostoUnidad                 = lote.CostoUnidad,
                                 FechaVencimiento            = lote.FechaVencimiento,
                                 CantidadActual              = lote.CantidadActual,
                                 IdUsuarioCreacion           = lote.IdUsuarioCreacion,
                                 NombreUsuarioCreacion       = usuarioCrea.NombreUsuario,
                                 FechaCreacion               = lote.FechaCreacion,
                                 IdUsuarioModificacion       = lote.IdUsuarioModificacion,
                                 NombreUsuarioModificacion   = usuarioModifica.NombreUsuario,
                                 FechaModificacion           = lote.FechaModificacion,
                                 Activo                      = lote.Activo
                             }).ToList();

            return Respuesta.Success(resultado, "", CodigosGlobales.EXITO);
        }

        public Respuesta<LoteDto> InsertarLote(LoteDto lote)
        { 
            if (DatosSesion.UsuarioLogueadoId == 0)
                return Respuesta.Fault<LoteDto>(MensajesAcciones.INVALIDO_INICIO_SESION, CodigosGlobales.ERROR);
            
            try
            {
                lote.IdLote = (int)ValoresDefecto.IdIngreso;

                var productos = _unitOfWork.Repository<Producto>().AsQueryable().Where(e => e.Activo).ToList();
                Respuesta<bool> loteValido = _loteDomainService.EsLoteValidoInsertar(productos, lote.IdProducto);

                if (!loteValido.Ok)
                    return Respuesta.Fault<LoteDto>(loteValido.Mensaje, CodigosGlobales.ADVERTENCIA);

                var loteMapeado = _mapper.Map<Lote>(lote);

                loteMapeado.CantidadActual          = lote.CantidadIngresada;
                loteMapeado.IdUsuarioCreacion       = DatosSesion.UsuarioLogueadoId;
                loteMapeado.FechaCreacion           = DateTime.Now;
                loteMapeado.IdUsuarioModificacion   = null;
                loteMapeado.FechaModificacion       = null;
                loteMapeado.Activo                  = true;

                _unitOfWork.Repository<Lote>().Add(loteMapeado);
                _unitOfWork.SaveChanges();

                return Respuesta.Success(lote, MensajesAcciones.EXITO_INSERTAR, CodigosGlobales.EXITO);
            }
            catch (Exception)
            {
                return Respuesta<LoteDto>.Fault(MensajesAcciones.ERROR);
            }
        }

        public Respuesta<LoteDto> EditarLote(LoteDto lote)
        {
            if (DatosSesion.UsuarioLogueadoId == 0)
                return Respuesta.Fault<LoteDto>(MensajesAcciones.INVALIDO_INICIO_SESION, CodigosGlobales.ERROR);

            try
            {
                var productos = _unitOfWork.Repository<Producto>().AsQueryable().Where(e => e.Activo).ToList();
                var lotes = _unitOfWork.Repository<Lote>().AsQueryable().Where(e => e.Activo).ToList();

                Respuesta<bool> loteValido = _loteDomainService.EsloteValidoEditar(productos, lote.IdProducto, lotes , lote.IdLote);
                if (!loteValido.Ok) {
                    return Respuesta.Fault<LoteDto>(loteValido.Mensaje, CodigosGlobales.ADVERTENCIA);
                }

                var loteEditado = _unitOfWork.Repository<Lote>().FirstOrDefault(x => x.IdLote == lote.IdLote);

                if(loteEditado != null) {
                    loteEditado.IdProducto              = lote.IdProducto;
                    loteEditado.CantidadIngresada       = lote.CantidadIngresada;
                    loteEditado.CostoUnidad             = lote.CostoUnidad;
                    loteEditado.FechaVencimiento        = lote.FechaVencimiento;
                    loteEditado.FechaModificacion       = DateTime.Now;
                    loteEditado.IdUsuarioModificacion   = DatosSesion.UsuarioLogueadoId;
                }
                
                _unitOfWork.SaveChanges();
                return Respuesta.Success(lote, MensajesAcciones.EXITO_EDITAR, CodigosGlobales.EXITO);
            }
            catch 
            {
                return Respuesta<LoteDto>.Fault(MensajesAcciones.ERROR);
            }
        }

        public Respuesta<LoteDto> EliminarLote(LoteDto lote)
        {
            if (DatosSesion.UsuarioLogueadoId == 0)
                return Respuesta.Fault<LoteDto>(MensajesAcciones.INVALIDO_INICIO_SESION, CodigosGlobales.ERROR);

            try
            {
                var loteEliminado = _unitOfWork.Repository<Lote>().FirstOrDefault(x => x.IdLote == lote.IdLote);

                if (loteEliminado != null)
                    loteEliminado.Activo = false;
                else
                    Respuesta.Fault<LoteDto>(MensajesCamposInvalidos.IdNoEncontrado("lote"), CodigosGlobales.ADVERTENCIA);

                _unitOfWork.SaveChanges();
                return Respuesta.Success(lote, MensajesAcciones.EXITO_ELIMINAR, CodigosGlobales.EXITO);
            }
            catch
            {
                return Respuesta<LoteDto>.Fault(MensajesAcciones.ERROR);
            }
        }

       

        
    }
}
