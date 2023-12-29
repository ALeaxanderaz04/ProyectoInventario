using AcademiaFs.ProyectoInventario.WebApi._Common;
using AcademiaFs.ProyectoInventario.WebApi._Features.Productos.Dto;
using AcademiaFs.ProyectoInventario.WebApi._Features.Sucursales.Dto;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using AcademiaFs.ProyectoInventario.WebApi.Utility;
using AcademiaFS.ProyectoTransporte._Common;
using AcademiaFS.ProyectoTransporte._Features.Login;
using AutoMapper;
using Farsiman.Application.Core.Standard.DTOs;
using Farsiman.Domain.Core.Standard.Repositories;

namespace AcademiaFs.ProyectoInventario.WebApi._Features.Productos
{
    public class ProductoAppService : IProductoAppServicecs
    {
        private readonly    IUnitOfWork             _unitOfWork;
        readonly            IMapper                 _mapper;
        private readonly    ProductoDomainService   _productoDomain;

        public ProductoAppService(  IMapper                 mapper,
                                    UnitOfworkBuilder       unitOfWorkBuilder,
                                    ProductoDomainService   productoDomain)
        {
            _mapper             = mapper;
            _unitOfWork         = unitOfWorkBuilder.BuilderDB01();
            _productoDomain     = productoDomain;
        }

        public Respuesta<List<ProductoListadoDto>> ListadoProductos()
        {
            var resultado = (from producto in _unitOfWork.Repository<Producto>().AsQueryable()
                             join usuarioCrea in _unitOfWork.Repository<Usuario>().AsQueryable()
                             on producto.IdUsuarioCreacion equals usuarioCrea.IdUsuario
                             join usuarioModifica in _unitOfWork.Repository<Usuario>().AsQueryable()
                             on producto.IdUsuarioModificacion equals usuarioModifica.IdUsuario into modificacion
                             from usuarioModifica in modificacion.DefaultIfEmpty()
                             where (producto.Activo)
                             select new ProductoListadoDto
                             {
                                 IdProducto                  = producto.IdProducto,
                                 Nombre                      = producto.Nombre,
                                 IdUsuarioCreacion           = producto.IdUsuarioCreacion,
                                 NombreUsuarioCreacion       = usuarioCrea.NombreUsuario,
                                 FechaCreacion               = producto.FechaCreacion,
                                 IdUsuarioModificacion       = producto.IdUsuarioModificacion,
                                 NombreUsuarioModificacion   = usuarioModifica.NombreUsuario,
                                 FechaModificacion           = producto.FechaModificacion,
                                 Activo                      = producto.Activo

                             }).ToList();

            return Respuesta.Success(resultado, "", CodigosGlobales.EXITO);
        }

        public Respuesta<ProductoDto> InsertarProducto(ProductoDto producto)
        {
            if (DatosSesion.UsuarioLogueadoId == 0)
                return Respuesta.Fault<ProductoDto>(MensajesAcciones.INVALIDO_INICIO_SESION, CodigosGlobales.ERROR);
            try
            {
                producto.IdProducto = (int)ValoresDefecto.IdIngreso;
                var productoMapeado = _mapper.Map<Producto>(producto);

                productoMapeado.IdUsuarioCreacion       = DatosSesion.UsuarioLogueadoId;
                productoMapeado.FechaCreacion           = DateTime.Now;
                productoMapeado.IdUsuarioModificacion   = null;
                productoMapeado.FechaModificacion       = null;
                productoMapeado.Activo                  = true;

                _unitOfWork.Repository<Producto>().Add(productoMapeado);
                _unitOfWork.SaveChanges();

                return Respuesta.Success(producto, MensajesAcciones.EXITO_INSERTAR, CodigosGlobales.EXITO);
            }
            catch
            {
                return Respuesta<ProductoDto>.Fault(MensajesAcciones.ERROR);
            }
        }

        public Respuesta<ProductoDto> EditarProducto(ProductoDto producto)
        {
            if (DatosSesion.UsuarioLogueadoId == 0)
                return Respuesta.Fault<ProductoDto>(MensajesAcciones.INVALIDO_INICIO_SESION, CodigosGlobales.ERROR);
            
            try
            {
                var productos = _unitOfWork.Repository<Producto>().AsQueryable().Where(e => e.Activo).ToList();

                Respuesta<bool> productoExiste = _productoDomain.ProductoExiste(productos,producto.IdProducto);
                if (!productoExiste.Ok)
                    return Respuesta.Fault<ProductoDto>(productoExiste.Mensaje, CodigosGlobales.ADVERTENCIA);

                var productoEditado = _unitOfWork.Repository<Producto>().FirstOrDefault(x => x.IdProducto == producto.IdProducto);
                if (productoEditado != null)
                {
                    productoEditado.Nombre                  = producto.Nombre;
                    productoEditado.IdUsuarioModificacion   = DatosSesion.UsuarioLogueadoId;
                    productoEditado.FechaModificacion       = DateTime.Now;
                }

                _unitOfWork.SaveChanges();
                return Respuesta.Success(producto, MensajesAcciones.EXITO_EDITAR, CodigosGlobales.EXITO);
            }
            catch (Exception)
            {
                return Respuesta<ProductoDto>.Fault(MensajesAcciones.ERROR);
            }
        }

        public Respuesta<ProductoDto> EliminarProducto(ProductoDto producto)
        {
            if (DatosSesion.UsuarioLogueadoId == 0)
                return Respuesta.Fault<ProductoDto>(MensajesAcciones.INVALIDO_INICIO_SESION, CodigosGlobales.ERROR);

            try
            {
                var productoEliminado = _unitOfWork.Repository<Producto>().FirstOrDefault(x => x.IdProducto == producto.IdProducto);
                if (productoEliminado != null)
                    productoEliminado.Activo = false;
                else
                   return Respuesta.Fault<ProductoDto>(MensajesCamposInvalidos.IdNoEncontrado("producto"), CodigosGlobales.ADVERTENCIA);

                _unitOfWork.SaveChanges();
                return Respuesta.Success(producto, MensajesAcciones.EXITO_ELIMINAR, CodigosGlobales.EXITO);
            }
            catch (Exception)
            {
                return Respuesta<ProductoDto>.Fault(MensajesAcciones.ERROR);
            }
        }
    }
}
