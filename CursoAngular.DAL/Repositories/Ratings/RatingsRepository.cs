using CursoAngular.BOL.Entities;
using CursoAngular.Repository.Ratings;
using Microsoft.EntityFrameworkCore;

namespace CursoAngular.DAL.Repositories.Ratings
{
    public class RatingsRepository : IRatingsRepository
    {
        private readonly CursoAngularDbContext context;

        public RatingsRepository(CursoAngularDbContext context)
        {
            this.context = context;
        }

        public async Task<RatingEntity> GetAsync(int movieId, string userId)
        {
            return await context.Ratings
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.MovieId.Equals(movieId) && x.UserId.Equals(userId));
        }

        public async Task<double> GetAverageAsync(int movieId)
        {
            return await context.Ratings.Where(x => x.MovieId.Equals(movieId)).AverageAsync(x => x.Scoring);
        }
    }
}
