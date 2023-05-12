using System.Linq.Expressions;

namespace CursoAngular.Repository;
public interface IGenericRepository<TEntity>
{
    void Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(int entityId);
    void Delete(TEntity entity);
    Task<TEntity> GetById(int entityId);
    Task<bool> Exists(int entityId);
    Task<List<TEntity>> Get<TKey>(int skipCount, int takeCount, Expression<Func<TEntity, TKey>> orderBy);
    Task<int> GetCount();
    Task<List<TEntity>> Get<TKey>(Expression<Func<TEntity, TKey>> orderBy);
}
