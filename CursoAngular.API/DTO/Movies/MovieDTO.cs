using CursoAngular.API.DTO.Cinemas;
using CursoAngular.API.DTO.StarsMovies;

namespace CursoAngular.API.DTO.Movies
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? PosterUrl { get; set; }
        public string? TrailerUrl { get; set; }
        public string? MpaaRating { get; set; }

        public List<GenreDTO>? Genres { get; set; }
        public List<CastDTO>? Cast { get; set; }
        public List<CinemaDTO>? Cinemas { get; set; }
    }
}
