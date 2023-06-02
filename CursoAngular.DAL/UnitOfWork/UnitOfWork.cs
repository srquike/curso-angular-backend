using CursoAngular.DAL.Repositories.Generics;
using CursoAngular.DAL.Repositories.Movies;
using CursoAngular.DAL.Repositories.Stars;
using CursoAngular.Repository;
using CursoAngular.Repository.Movies;
using CursoAngular.Repository.Stars;
using CursoAngular.UOW;
using System.Collections;

namespace CursoAngular.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CursoAngularDbContext _dbContext;

        private IStarsRepository? _starsRepository;
        private IMoviesRespository? _moviesRespository;

        private Hashtable? _repositories;
        private bool _dbContextDisposed;

        public IStarsRepository StarsRepository => _starsRepository ??= new StarsRepository(_dbContext);
        public IMoviesRespository MoviesRespository => _moviesRespository ??= new MoviesRepository(_dbContext);

        public UnitOfWork(CursoAngularDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbContextDisposed = false;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_dbContextDisposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            _dbContextDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            var entityName = typeof(TEntity).Name;

            _repositories ??= new Hashtable();

            if (!_repositories.ContainsKey(entityName))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);
                _repositories.Add(entityName, repositoryInstance);
            }

            return (IGenericRepository<TEntity>)_repositories[entityName];
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}
