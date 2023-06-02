using CursoAngular.BOL.Entities;
using CursoAngular.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CursoAngular.DAL.Repositories.Generics
{
    internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly CursoAngularDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;
        private IQueryable<TEntity> _entities;

        public GenericRepository(CursoAngularDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
            _entities = _dbSet.AsNoTracking();
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

        public async Task<TEntity> Get(int entityId)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id.Equals(entityId));
        }

        public async Task<int> GetCount()
        {
            return await _dbSet.AsNoTracking().CountAsync();
        }

        public async Task<bool> Exists(int entityId)
        {
            return await _dbSet.AsNoTracking().AnyAsync(e => e.Id.Equals(entityId));
        }

        public void Update(TEntity entity)
        {
            if (_dbSet.Entry(entity).State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            _dbSet.Entry(entity).State = EntityState.Modified;
        }

        public Task<List<TEntity>> Get()
        {
            var result = _entities.ToListAsync();
            _entities = _dbSet.AsNoTracking();

            return result;
        }

        public IGenericRepository<TEntity> Filter(Expression<Func<TEntity, bool>> expression)
        {
            _entities = _entities.Where(expression);
            return this;
        }

        public IGenericRepository<TEntity> Order<TKey>(Expression<Func<TEntity, TKey>> expression)
        {
            _entities = _entities.OrderBy(expression);
            return this;
        }

        public IGenericRepository<TEntity> Take(int count)
        {
            _entities = _entities.Take(count);
            return this;
        }

        public IGenericRepository<TEntity> Skip(int count)
        {
            _entities = _entities.Skip(count);
            return this;
        }
    }
}
