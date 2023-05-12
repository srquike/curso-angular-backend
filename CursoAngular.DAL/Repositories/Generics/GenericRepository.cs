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

        public async Task<List<TEntity>> Get<TKey>(int skipCount, int takeCount, Expression<Func<TEntity, TKey>> orderBy)
        {
            return await _dbSet
                .AsNoTracking()
                .OrderBy(orderBy)
                .Skip(skipCount)
                .Take(takeCount)
                .ToListAsync();
        }

        public async Task<List<TEntity>> Get<TKey>(Expression<Func<TEntity, TKey>> orderBy)
        {
            return await _dbSet
                .AsNoTracking()
                .OrderBy(orderBy)
                .ToListAsync();
        }

        public async Task<TEntity> GetById(int entityId)
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
    }
}
