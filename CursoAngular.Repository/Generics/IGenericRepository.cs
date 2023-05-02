namespace CursoAngular.Repository;
public interface IGenericRepository<TEntity>
{
    void Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(int entityId);
    void Delete(TEntity entity);
    Task<TEntity> GetById(int entityId);
    Task<IReadOnlyList<TEntity>> Get();
    Task<bool> Exists(int entityId);
}
