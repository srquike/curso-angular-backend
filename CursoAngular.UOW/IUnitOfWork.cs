using CursoAngular.Repository;

namespace CursoAngular.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<bool> SaveChangesAsync();
    }
}