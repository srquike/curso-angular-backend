using CursoAngular.BOL.Entities;

namespace CursoAngular.Repository.Ratings
{
    public interface IRatingsRepository
    {
        Task<RatingEntity> GetAsync(int movieId, string userId);
        Task<double> GetAverageAsync(int movieId);
    }
}
