using Farsiman.Domain.Core.Standard.Repositories;
using System.Linq.Expressions;

namespace AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario.Repository
{
    public class EntityRepository<IEntity> : IRepository<IEntity> where IEntity : class
    {
        public void Add(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<IEntity> AsQuerable()
        {
            throw new NotImplementedException();
        }

        public IEntity? FirstOrDefault(Expression<Func<IEntity, bool>> query)
        {
            throw new NotImplementedException();
        }

        public List<IEntity> where(Expression<Func<IEntity, bool>> query)
        {
            throw new NotImplementedException();
        }
    }
}
