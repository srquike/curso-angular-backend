using CursoAngular.BOL;

namespace CursoAngular.Repository.Stars
{
    public interface IStarsRepository
    {
        Task<List<StarEntity>> GetByName(string name);
    }
}
