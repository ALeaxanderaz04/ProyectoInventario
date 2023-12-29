using AcademiaFs.ProyectoInventario.WebApi._Features.Lotes.Dto;
using AcademiaFs.ProyectoInventario.WebApi._Features.Productos.Dto;
using AcademiaFs.ProyectoInventario.WebApi._Features.Salidas.Dto;
using AcademiaFs.ProyectoInventario.WebApi._Features.Sucursales.Dto;
using AcademiaFs.ProyectoInventario.WebApi._Features.Usuarios.Dto;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using AutoMapper;

namespace AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<UsuarioDto, Usuario>().ReverseMap();
            CreateMap<LoteDto, Lote>().ReverseMap();
            CreateMap<ProductoDto, Producto>().ReverseMap();
            CreateMap<SucursalDto, Sucursal>().ReverseMap();
            CreateMap<SalidaDto, Salida>().ReverseMap();
            CreateMap<SalidaDetalleDto, SalidaDetalle>().ReverseMap();
        }
    }
}
