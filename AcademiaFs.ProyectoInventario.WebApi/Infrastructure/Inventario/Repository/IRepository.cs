using System.Linq.Expressions;

namespace AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Repository
{
    public interface IRepository<T>
    {
        void Add(T entity);
        IQueryable<T> AsQuerable();
        List<T> where(Expression<Func<T, bool>> query);
        T? FirstOrDefault(Expression<Func<T, bool>> query);
    }
}
