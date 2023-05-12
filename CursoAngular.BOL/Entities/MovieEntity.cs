using CursoAngular.BOL.Entities;

namespace CursoAngular.BOL;
public class MovieEntity : BaseEntity
{
    public string? Title { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string? PosterUrl { get; set; }
    public string? TrailerUrl { get; set; }
    public string? MpaaRating { get; set; }

    public virtual ICollection<GenreEntity> Genres { get; set; } = new List<GenreEntity>();
    public virtual ICollection<StarEntity> Cast { get; set; } = new List<StarEntity>();
    public virtual ICollection<CinemaEntity> Cinemas { get; set; } = new List<CinemaEntity>();

    public virtual ICollection<StarMovieEntity> StarMovies { get; set; } = new List<StarMovieEntity>();
    public virtual ICollection<GenreMovieEntity> GenreMovies { get; set; } = new List<GenreMovieEntity>();
    public virtual ICollection<MovieCinemaEntity> CinemaMovies { get; set; } = new List<MovieCinemaEntity>();
}
