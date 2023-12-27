using AcademiaFs.ProyectoInventario.WebApi._Features.Usuarios;
using AcademiaFs.ProyectoInventario.WebApi._Features.Usuarios.Dto;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using AcademiaFS.ProyectoTransporte._Common;
using AcademiaFS.ProyectoTransporte._Features.Login;
using AcademiaFS.ProyectoTransporte.Utility;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;

namespace AcademiaFs.ProyectoInventario.WebApi._Features.Login
{
    public class LoginService
    {
        private readonly IUnitOfWork _unitOfWork;
        readonly IMapper _mapper;
        private readonly UsuarioService _usuarioService;

        public LoginService(IMapper mapper, UnitOfworkBuilder unitOfWorkBuilder, UsuarioService usuarioService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWorkBuilder.BuilderDB01();
            _usuarioService = usuarioService;
        }

        public Respuesta<List<UsuarioDto>> InicarSesion(UsuarioDto usuarioDto)
        {

            var contrasenaTemporal = IncriptarContrasenia.GenerateSHA512Hash(usuarioDto.Contrasena ?? "");
            var constrasenia = IncriptarContrasenia.HashedString(contrasenaTemporal);


            var login = (from usuarios in _unitOfWork.Repository<Usuario>().AsQueryable()
                         where usuarios.NombreUsuario == usuarioDto.NombreUsuario &&
                                usuarios.Contrasena == constrasenia &&
                                usuarios.Activo
                         select new UsuarioDto
                         {
                             IdUsuario = usuarios.IdUsuario,
                             NombreUsuario = usuarios.NombreUsuario,
                             Contrasena = "***********",
                             IdRol = usuarios.IdRol,
                             IdEmpleado = usuarios.IdEmpleado,
                             EsAdmin = usuarios.EsAdmin
                         }).ToList();


            if (login.Any())
            {
                foreach (var item in login)
                {
                    DatosSesion.UsuarioLogueadoId = item.IdUsuario;
                    DatosSesion.UsuarioLogueadoEsAdmin = item.EsAdmin;
                }
            }

            return Respuesta.Success(login, "", CodigosGlobales.EXITO);
        }
    }
}
