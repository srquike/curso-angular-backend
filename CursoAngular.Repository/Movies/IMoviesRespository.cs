using CursoAngular.BOL;
using System.Linq.Expressions;

namespace CursoAngular.Repository.Movies
{
    public interface IMoviesRespository
    {
        Task<MovieEntity> Get(int id, bool asNoTracking);
    }
}
