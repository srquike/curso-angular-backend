using CursoAngular.DAL.Repositories.Generics;
using CursoAngular.DAL.Repositories.Movies;
using CursoAngular.DAL.Repositories.Ratings;
using CursoAngular.DAL.Repositories.Stars;
using CursoAngular.DAL.Repositories.Users;
using CursoAngular.Repository;
using CursoAngular.Repository.Movies;
using CursoAngular.Repository.Ratings;
using CursoAngular.Repository.Stars;
using CursoAngular.Repository.Users;
using CursoAngular.UOW;
using Microsoft.AspNetCore.Identity;
using System.Collections;

namespace CursoAngular.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CursoAngularDbContext _dbContext;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;

        private IStarsRepository? _starsRepository;
        private IMoviesRespository? _moviesRespository;
        private IUsersRepository? _usersRepository;
        private IRatingsRepository? ratingsRepository;

        private Hashtable? _repositories;
        private bool _dbContextDisposed;

        public IStarsRepository StarsRepository => _starsRepository ??= new StarsRepository(_dbContext);
        public IMoviesRespository MoviesRespository => _moviesRespository ??= new MoviesRepository(_dbContext);
        public IUsersRepository UsersRepository => _usersRepository ??= new UsersRepository(_dbContext, userManager, signInManager);
        public IRatingsRepository RatingsRepository => ratingsRepository ??= new RatingsRepository(_dbContext);

        public UnitOfWork(CursoAngularDbContext dbContext, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _dbContext = dbContext;
            _dbContextDisposed = false;
            this.userManager = userManager;
            this.signInManager = signInManager;
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
