using CursoAngular.BOL.Entities;

namespace CursoAngular.BOL;
public class GenreEntity : BaseEntity
{
    public string? Name { get; set; }

    public virtual ICollection<GenreMovieEntity> GenreMovies { get; set; } = new List<GenreMovieEntity>();
}
