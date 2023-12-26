
using Farsiman.Infraestructure.Core.Entity.Standard;

namespace AcademiaFs.ProyectoInventario.WebApi.Infrastructure.Inventario
{
    public class UnitOfworkBuilder
    {
        public readonly IServiceProvider _serviceProvider;

        public UnitOfworkBuilder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public UnitOfWork BuilderDB01()
        {
            InventarioCaazContext context = _serviceProvider.GetService<InventarioCaazContext>() ?? throw new NullReferenceException();
            return new UnitOfWork(context);
        }
    }
}
