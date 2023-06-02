using CursoAngular.API.DTO.Cinemas;
using CursoAngular.API.DTO.StarsMovies;

namespace CursoAngular.API.DTO.Movies
{
    public class EditMovieDTO
    {
        public MovieDTO? Movie { get; set; }
        public List<GenreDTO>? Genres { get; set; }
        public List<GenreDTO>? NoSelectedGenres { get; set; }
        public List<CinemaDTO>? Cinemas { get; set; }
        public List<CinemaDTO>? NoSelectedCinemas { get; set; }
        public List<CastDTO>? Cast { get; set; }
    }
}
