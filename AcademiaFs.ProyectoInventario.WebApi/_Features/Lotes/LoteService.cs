using AcademiaFs.ProyectoInventario.WebApi._Common;
using AcademiaFs.ProyectoInventario.WebApi._Features.Lotes.Dto;
using AcademiaFs.ProyectoInventario.WebApi._Features.Usuarios;
using AcademiaFs.ProyectoInventario.WebApi._Features.Usuarios.Dto;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using AcademiaFs.ProyectoInventario.WebApi.Utility;
using AcademiaFS.ProyectoTransporte._Common;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
using System;

namespace AcademiaFs.ProyectoInventario.WebApi._Features.Lotes
{
    public class LoteService
    {
        private readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        private readonly ExistenciaDatos _existenciaDatos;

        public LoteService(IMapper mapper,
                                UnitOfworkBuilder unitOfWorkBuilder,
                                ExistenciaDatos existenciaDatos)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWorkBuilder.BuilderDB01();
            _existenciaDatos  = existenciaDatos;
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
            _unitOfWork.BeginTransaction();
            try
            {
                string llavesForaneasValidas = ValidarInsertarLlavesForaneas(lote);
                if (llavesForaneasValidas.Contains("Advertencia"))
                {
                    return Respuesta.Fault<LoteDto>(llavesForaneasValidas, CodigosGlobales.ADVERTENCIA);
                }

                var loteMapeado = _mapper.Map<Lote>(lote);

                loteMapeado.IdUsuarioCreacion       = 1;
                loteMapeado.FechaCreacion           = DateTime.Now;
                loteMapeado.IdUsuarioModificacion   = null;
                loteMapeado.FechaModificacion       = null;
                loteMapeado.Activo                  = true;

                _unitOfWork.Repository<Lote>().Add(loteMapeado);
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();

                return Respuesta.Success(lote, MensajesAccionesInvalidas.EXITO_INSERTAR, CodigosGlobales.EXITO);
            }
            catch (Exception)
            {
                _unitOfWork.RollBack();
                return Respuesta<LoteDto>.Fault(MensajesAccionesInvalidas.ERROR);
            }
        }

        public Respuesta<LoteDto> EditarLote(LoteDto lote)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                string llavesForaneasValidas = ValidarEditarLlavesForaneas(lote);
                if (llavesForaneasValidas.Contains("Advertencia")) {
                    return Respuesta.Fault<LoteDto>(llavesForaneasValidas, CodigosGlobales.ADVERTENCIA);
                }

                var loteEditado = _unitOfWork.Repository<Lote>().FirstOrDefault(x => x.IdLote == lote.IdLote);

                loteEditado.IdProducto = lote.IdProducto;
                loteEditado.CantidadIngresada = lote.CantidadIngresada;
                loteEditado.CostoUnidad = lote.CostoUnidad;
                loteEditado.FechaVencimiento = lote.FechaVencimiento;
                loteEditado.FechaModificacion = DateTime.Now;
                loteEditado.IdUsuarioModificacion = 1;

                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                return Respuesta.Success(lote, MensajesAccionesInvalidas.EXITO_EDITAR, CodigosGlobales.EXITO);
            }
            catch 
            {
                _unitOfWork.RollBack();
                return Respuesta<LoteDto>.Fault(MensajesAccionesInvalidas.ERROR);
            }
        }

        public Respuesta<LoteDto> EliminarLote(LoteDto lote)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                bool loteExiste = _existenciaDatos.ValidarLoteExiste(lote.IdLote);
                if (!loteExiste){
                    return Respuesta.Success(lote, MensajesCamposInvalidos.IdNoEncontrado("lote"), CodigosGlobales.EXITO);
                }

                var loteEditado = _unitOfWork.Repository<Lote>().FirstOrDefault(x => x.IdLote == lote.IdLote);

                loteEditado.Activo = false;

                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();
                return Respuesta.Success(lote, MensajesAccionesInvalidas.EXITO_EDITAR, CodigosGlobales.EXITO);
            }
            catch
            {
                _unitOfWork.RollBack();
                return Respuesta<LoteDto>.Fault(MensajesAccionesInvalidas.ERROR);
            }
        }

        public string ValidarInsertarLlavesForaneas(LoteDto lote)
        {
            bool productoExiste = _existenciaDatos.ValidarProductoExiste(lote.IdProducto);
            if (!productoExiste){
                return MensajesCamposInvalidos.IdNoEncontrado("Producto");
            }
            return string.Empty;
        }

        public string ValidarEditarLlavesForaneas(LoteDto lote)
        {
            bool loteExiste = _existenciaDatos.ValidarLoteExiste(lote.IdLote);
            if (!loteExiste)
            {
                return MensajesCamposInvalidos.IdNoEncontrado("lote");
            }

            bool productoExiste = _existenciaDatos.ValidarProductoExiste(lote.IdProducto);
            if (!productoExiste)
            {
                return MensajesCamposInvalidos.IdNoEncontrado("producto");
            }
            return string.Empty;
        }
    }
}
