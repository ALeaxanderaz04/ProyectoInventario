using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario;
using AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Entities;
using AutoMapper;
using Farsiman.Domain.Core.Standard.Repositories;

namespace AcademiaFs.ProyectoInventario.WebApi.Utility
{
    public class ExistenciaDatos
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExistenciaDatos(UnitOfworkBuilder unitOfWorkBuilder)
        {
            _unitOfWork = unitOfWorkBuilder.BuilderDB01();
        }

        public bool ValidarEmpleadoExiste(int? idEmpleado)
        {
            var empleados = _unitOfWork.Repository<Empleado>().AsQueryable();
            var empleadoExiste = empleados.FirstOrDefault(e => e.IdEmpleado == idEmpleado);

            if(empleadoExiste != null) { 
                return true;
            }
            return false;
        }

        public bool ValidarRolExiste(int? idRol)
        {
            var roles = _unitOfWork.Repository<Rol>().AsQueryable();
            var rolExiste = roles.FirstOrDefault(e => e.IdRol == idRol);

            if (rolExiste != null){
                return true;
            }
            return false;
        }

        public bool ValidarProductoExiste(int? idProducto)
        {
            var productos = _unitOfWork.Repository<Producto>().AsQueryable();
            var productoExiste = productos.FirstOrDefault(e => e.IdProducto == idProducto);

            if (productoExiste != null){
                return true;
            }
            return false;
        }

        public bool ValidarLoteExiste(int? idLote)
        {
            var lotes = _unitOfWork.Repository<Lote>().AsQueryable();
            var loteExiste = lotes.FirstOrDefault(e => e.IdLote == idLote);

            if (loteExiste != null)
            {
                return true;
            }
            return false;
        }

        public bool ValidarMunicipioExiste(string idMunicipio)
        {
            var municipios = _unitOfWork.Repository<Municipio>().AsQueryable();
            var municicioExiste = municipios.FirstOrDefault(e => e.IdMunicipio == idMunicipio);

            if (municicioExiste != null)
            {
                return true;
            }
            return false;
        }

    }
}
