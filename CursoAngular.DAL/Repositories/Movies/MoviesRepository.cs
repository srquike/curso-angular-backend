using CursoAngular.BOL;
using CursoAngular.Repository.Movies;
using Microsoft.EntityFrameworkCore;

namespace CursoAngular.DAL.Repositories.Movies
{
    public class MoviesRepository : IMoviesRespository
    {
        private readonly CursoAngularDbContext context;

        public MoviesRepository(CursoAngularDbContext context)
        {
            this.context = context;
        }

        public async Task<MovieEntity> Get(int id, bool asNoTracking)
        {
            var result = context.Movies
                .Include(m => m.GenreMovies).ThenInclude(gm => gm.Genre)
                .Include(m => m.CinemaMovies).ThenInclude(cm => cm.Cinema)
                .Include(m => m.Casting).ThenInclude(sm => sm.Star);

            if (asNoTracking)
            {
                return await result.AsNoTracking().FirstOrDefaultAsync(m => m.Id.Equals(id));
            }

            return await result.FirstOrDefaultAsync(m => m.Id.Equals(id));
        }
    }
}
