using System.Linq.Expressions;

namespace CursoAngular.Repository;
public interface IGenericRepository<TEntity>
{
    void Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(int entityId);
    void Delete(TEntity entity);
    Task<TEntity> Get(int entityId);
    Task<bool> Exists(int entityId);
    Task<int> GetCount();
    Task<List<TEntity>> Get();
    IGenericRepository<TEntity> Filter(Expression<Func<TEntity, bool>> expression);
    IGenericRepository<TEntity> Order<TKey>(Expression<Func<TEntity, TKey>> expression);
    IGenericRepository<TEntity> Take(int count);
    IGenericRepository<TEntity> Skip(int count);
}
