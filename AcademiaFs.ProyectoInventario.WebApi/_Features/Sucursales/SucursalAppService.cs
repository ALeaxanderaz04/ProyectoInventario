using AcademiaFs.ProyectoInventario.WebApi._Common;
using AcademiaFs.ProyectoInventario.WebApi._Features.Productos.Dto;
using AcademiaFs.ProyectoInventario.WebApi._Features.Sucursales.Dto;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using AcademiaFS.ProyectoTransporte._Common;
using AcademiaFS.ProyectoTransporte._Features.Login;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;

namespace AcademiaFs.ProyectoInventario.WebApi._Features.Sucursales
{
    public class SucursalAppService : ISucursalAppService
    {
        private readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        private readonly SucursalDomainService _domainService;

        public SucursalAppService(IMapper mapper,
                                UnitOfworkBuilder unitOfWorkBuilder,
                                SucursalDomainService domainService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWorkBuilder.BuilderDB01();
            _domainService = domainService;
        }
      
        public Respuesta<SucursalDto> InsertarSucursal(SucursalDto sucursal)
        {
            if (DatosSesion.UsuarioLogueadoId == 0)
                return Respuesta.Fault<SucursalDto>(MensajesAcciones.INVALIDO_INICIO_SESION, CodigosGlobales.ERROR);

            try
            {
                sucursal.IdSucursal = (int)ValoresDefecto.IdIngreso;

                List<Municipio> municipios = _unitOfWork.Repository<Municipio>().AsQueryable().Where(e => e.Activo).ToList();

                Respuesta<bool> esSucursalValida = _domainService.EsSucursalValidaInsertar(sucursal, municipios);
                if (!esSucursalValida.Ok)
                    return Respuesta.Fault<SucursalDto>(esSucursalValida.Mensaje, CodigosGlobales.ADVERTENCIA);
                

                var sucursalMapeada = _mapper.Map<Sucursal>(sucursal);

                sucursalMapeada.IdUsuarioCreacion       = DatosSesion.UsuarioLogueadoId;
                sucursalMapeada.FechaCreacion           = DateTime.Now;
                sucursalMapeada.IdUsuarioModificacion   = null;
                sucursalMapeada.FechaModificacion       = null;
                sucursalMapeada.Activo                  = true;

                _unitOfWork.Repository<Sucursal>().Add(sucursalMapeada);
                if (!_unitOfWork.SaveChanges())
                    return Respuesta<SucursalDto>.Fault(MensajesAcciones.ERROR_INSERTAR);
                

                return Respuesta.Success(sucursal, MensajesAcciones.EXITO_INSERTAR, CodigosGlobales.EXITO);
            }
            catch (Exception)
            {
                return Respuesta<SucursalDto>.Fault(MensajesAcciones.ERROR);
            }
        }

        public Respuesta<SucursalDto> EditarSucursal(SucursalDto sucursal)
        {
            if (DatosSesion.UsuarioLogueadoId == 0)
                return Respuesta.Fault<SucursalDto>(MensajesAcciones.INVALIDO_INICIO_SESION, CodigosGlobales.ERROR);

            try
            {
                var municipios = _unitOfWork.Repository<Municipio>().AsQueryable().Where(e => e.Activo).ToList();
                var sucursales = _unitOfWork.Repository<Sucursal>().AsQueryable().Where(e => e.Activo).ToList();
                Respuesta<bool> esSucursalValida = _domainService.EsSucursalValidaEditar(sucursal, municipios, sucursales);

                if (!esSucursalValida.Data){
                    return Respuesta.Fault<SucursalDto>(esSucursalValida.Mensaje, CodigosGlobales.ADVERTENCIA);
                }

                var sucursaEditada = _unitOfWork.Repository<Sucursal>().FirstOrDefault(x => x.IdSucursal == sucursal.IdSucursal);

                if (sucursaEditada != null){
                    sucursaEditada.Nombre                   = sucursal.Nombre;
                    sucursaEditada.IdMunicipio              = sucursal.IdMunicipio;
                    sucursaEditada.Direccion                = sucursal.Direccion;
                    sucursaEditada.FechaModificacion        = DateTime.Now;
                    sucursaEditada.IdUsuarioModificacion    = DatosSesion.UsuarioLogueadoId;
                }

                _unitOfWork.SaveChanges();
                return Respuesta.Success(sucursal, MensajesAcciones.EXITO_EDITAR, CodigosGlobales.EXITO);
            }
            catch (Exception)
            {
                return Respuesta<SucursalDto>.Fault(MensajesAcciones.ERROR);
            }
        }

        public Respuesta<SucursalDto> EliminarSucursal(SucursalDto sucursal)
        {
            if (DatosSesion.UsuarioLogueadoId == 0)
                return Respuesta.Fault<SucursalDto>(MensajesAcciones.INVALIDO_INICIO_SESION, CodigosGlobales.ERROR);

            try
            {
                var sucursaEliminada = _unitOfWork.Repository<Sucursal>().FirstOrDefault(x => x.IdSucursal == sucursal.IdSucursal);

                if (sucursaEliminada != null)
                    sucursaEliminada.Activo = false;
                else
                    Respuesta.Fault<SucursalDto>(MensajesCamposInvalidos.IdNoEncontrado("sucursal"), CodigosGlobales.ADVERTENCIA);


                _unitOfWork.SaveChanges();
                return Respuesta.Success(sucursal, MensajesAcciones.EXITO_ELIMINAR, CodigosGlobales.EXITO);
            }
            catch (Exception)
            {
                return Respuesta<SucursalDto>.Fault(MensajesAcciones.ERROR);
            }
        }

        public Respuesta<List<SucursalListadoDto>> ListadoSucursales()
        {
            var resultado = (from sucursal in _unitOfWork.Repository<Sucursal>().AsQueryable()
                             join municipio in _unitOfWork.Repository<Municipio>().AsQueryable()
                             on sucursal.IdMunicipio equals municipio.IdMunicipio
                             join departamento in _unitOfWork.Repository<Departamento>().AsQueryable()
                             on municipio.IdDepartamento equals departamento.IdDepartamento
                             join usuarioCrea in _unitOfWork.Repository<Usuario>().AsQueryable()
                             on sucursal.IdUsuarioCreacion equals usuarioCrea.IdUsuario
                             join usuarioModifica in _unitOfWork.Repository<Usuario>().AsQueryable()
                             on sucursal.IdUsuarioModificacion equals usuarioModifica.IdUsuario into modificacion
                             from usuarioModifica in modificacion.DefaultIfEmpty()
                             where (sucursal.Activo)
                             select new SucursalListadoDto
                             {
                                 IdSucursal                  = sucursal.IdSucursal,
                                 Nombre                      = sucursal.Nombre,
                                 IdMunicipio                 = sucursal.IdMunicipio,
                                 NombreMunicipio             = municipio.Nombre,
                                 IdDepartamento              = municipio.IdDepartamento,
                                 NombreDepartamento          = departamento.Nombre,
                                 Direccion                   = sucursal.Direccion,
                                 IdUsuarioCreacion           = sucursal.IdUsuarioCreacion,
                                 NombreUsuarioCreacion       = usuarioCrea.NombreUsuario,
                                 FechaCreacion               = sucursal.FechaCreacion,
                                 IdUsuarioModificacion       = sucursal.IdUsuarioModificacion,
                                 NombreUsuarioModificacion   = usuarioModifica.NombreUsuario,
                                 FechaModificacion           = sucursal.FechaModificacion,
                                 Activo                      = sucursal.Activo

                             }).ToList();
            return Respuesta.Success(resultado, "", CodigosGlobales.EXITO);
        }
    }
}
