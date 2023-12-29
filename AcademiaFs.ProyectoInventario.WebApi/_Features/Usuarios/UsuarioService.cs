using AcademiaFs.ProyectoInventario.WebApi._Common;
using AcademiaFs.ProyectoInventario.WebApi._Features.Usuarios.Dto;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using AcademiaFs.ProyectoInventario.WebApi.Utility;
using AcademiaFS.ProyectoTransporte._Common;
using AcademiaFS.ProyectoTransporte._Features.Login;
using AcademiaFS.ProyectoTransporte.Utility;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;
using FluentValidation.Results;

namespace AcademiaFs.ProyectoInventario.WebApi._Features.Usuarios
{
    public class UsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        private readonly ExistenciaDatos _existenciaDatos;

        public UsuarioService(  IMapper mapper, 
                                UnitOfworkBuilder unitOfWorkBuilder,
                                ExistenciaDatos existenciaDatos)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWorkBuilder.BuilderDB01();
            _existenciaDatos  = existenciaDatos;
        }

        public Respuesta<List<UsuarioListadoDto>> ListadoUsuarios()
        {
            var resultado = (from usuario in _unitOfWork.Repository<Usuario>().AsQueryable()
                             join empleado in _unitOfWork.Repository<Empleado>().AsQueryable()
                             on usuario.IdEmpleado equals empleado.IdEmpleado
                             join rol in _unitOfWork.Repository<Rol>().AsQueryable()
                             on usuario.IdRol equals rol.IdRol
                             join usuarioCrea in _unitOfWork.Repository<Usuario>().AsQueryable()
                             on usuario.IdUsuarioCreacion equals usuarioCrea.IdUsuario
                             join usuarioModifica in _unitOfWork.Repository<Usuario>().AsQueryable()
                             on usuario.IdUsuarioModificacion equals usuarioModifica.IdUsuario into modificacion
                             from usuarioModifica in modificacion.DefaultIfEmpty()
                             where (usuario.Activo)
                             select new UsuarioListadoDto
                             {
                                 IdUsuario          = usuario.IdUsuario,
                                 NombreUsuario      = usuario.NombreUsuario,
                                 EsAdmin            = usuario.EsAdmin,
                                 IdEmpleado         = usuario.IdEmpleado,
                                 NombreEmpleado     = $"{empleado.Nombres} {empleado.Apellidos}",
                                 IdRol              = usuario.IdRol,
                                 NombreRol          = rol.Nombre,
                                 IdUsuarioCreacion  = usuario.IdUsuarioCreacion,
                                 NombreUsuarioCreacion = usuarioCrea.NombreUsuario,
                                 FechaCreacion      = usuario.FechaCreacion,
                                 IdUsuarioModificacion = usuario.IdUsuarioModificacion,
                                 NombreUsuarioModificacion = usuarioModifica.NombreUsuario
                             }).ToList();

            return Respuesta.Success(resultado, "", CodigosGlobales.EXITO);
        }

        public Respuesta<UsuarioDto> InsertarUsuario(UsuarioDto usuario)
        {
            if (DatosSesion.UsuarioLogueadoId == 0){
                return Respuesta.Fault<UsuarioDto>(MensajesAcciones.INVALIDO_INICIO_SESION, CodigosGlobales.ERROR);
            }
            _unitOfWork.BeginTransaction();
            try
            {
                string llavesForaneasValidas = ValidarInsertarLlavesForaneas(usuario);
                if (llavesForaneasValidas.Contains("Advertencia")){
                    return Respuesta.Fault<UsuarioDto>(llavesForaneasValidas, CodigosGlobales.ADVERTENCIA);
                } 

                var contraseniaTemporal = IncriptarContrasenia.GenerateSHA512Hash(usuario.Contrasena ?? "");
                usuario.Contrasena = IncriptarContrasenia.HashedString(contraseniaTemporal);

                var usuarioMapeado = _mapper.Map<Usuario>(usuario);

                usuario.IdUsuario                      = usuarioMapeado.IdUsuario;
                usuario.Contrasena                     = "**************";
                usuarioMapeado.IdUsuarioCreacion       = DatosSesion.UsuarioLogueadoId;
                usuarioMapeado.FechaCreacion           = DateTime.Now;
                usuarioMapeado.IdUsuarioModificacion   = null;
                usuarioMapeado.FechaModificacion       = null;
                usuarioMapeado.Activo                  = true;

                _unitOfWork.Repository<Usuario>().Add(usuarioMapeado);
                _unitOfWork.SaveChanges();
                _unitOfWork.Commit();

                return Respuesta.Success(usuario, MensajesAcciones.EXITO_INSERTAR, CodigosGlobales.EXITO);
            }
            catch (Exception)
            {
                _unitOfWork.RollBack();
                return Respuesta<UsuarioDto>.Fault(MensajesAcciones.ERROR);
            }
        }


        public string ValidarInsertarLlavesForaneas(UsuarioDto usuario)
        {
            bool empleadoExiste = _existenciaDatos.ValidarEmpleadoExiste(usuario.IdEmpleado);
            if (!empleadoExiste)
            {
                return MensajesCamposInvalidos.IdNoEncontrado("empleado");
            }

            bool rolExiste = _existenciaDatos.ValidarRolExiste(usuario.IdRol);
            if (!rolExiste)
            {
                return MensajesCamposInvalidos.IdNoEncontrado("rol");
            }
            return string.Empty;
        }
    }
}
