using CursoAngular.API.DTO.Cinemas;

namespace CursoAngular.API.DTO.Movies
{
    public class MovieResourcesDTO
    {
        public List<IndexCinemasDTO>? Cinemas { get; set; }
        public List<GenreDTO>? Genres { get; set; }
    }
}
