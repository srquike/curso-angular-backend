using CursoAngular.Repository;
using CursoAngular.Repository.Movies;
using CursoAngular.Repository.Stars;
using CursoAngular.Repository.Users;

namespace CursoAngular.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        IStarsRepository StarsRepository { get; }
        IMoviesRespository MoviesRespository { get; }
        IUsersRepository UsersRepository { get; }

        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
        Task<bool> SaveChangesAsync();
    }
}