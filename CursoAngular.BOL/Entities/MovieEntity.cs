namespace CursoAngular.BOL;
public class MovieEntity
{
    public int MovieId { get; set; }
    public string? Title { get; set; }
    public DateOnly? ReleaseDate { get; set; }
    public string? PosterUrl { get; set; }
    public string? TrailerUrl { get; set; }
    public string? MpaaRating { get; set; }

    public virtual ICollection<GenreEntity> Genres { get; set; } = new List<GenreEntity>();
    public virtual ICollection<StarEntity> Cast { get; set; } = new List<StarEntity>();
    public virtual ICollection<CinemaEntity> Cinemas { get; set; } = new List<CinemaEntity>();
}
