using CursoAngular.BOL.Entities;
using CursoAngular.Repository;
using Microsoft.EntityFrameworkCore;

namespace CursoAngular.DAL.Repositories.Generics
{
    internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly CursoAngularDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        private readonly string _idPropertyName;

        public GenericRepository(CursoAngularDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(int entityId)
        {
            var entity = _dbSet.Find(entityId);

            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public void Delete(TEntity entity)
        {
            if (_dbSet.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Remove(entity);
        }

        public async Task<bool> Exists(int entityId)
        {
            var entity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(entityId));

            return entity != null;
        }

        public async Task<IReadOnlyList<TEntity>> Get()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<TEntity> GetById(int entityId)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(entityId));
        }

        public void Update(TEntity entity)
        {
            if (_dbSet.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Entry(entity).State = EntityState.Modified;
        }
    }
}
