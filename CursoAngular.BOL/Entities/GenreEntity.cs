namespace CursoAngular.BOL;
public class GenreEntity
{
    public int GenreId { get; set; }
    public string? Name { get; set; }

    public virtual ICollection<MovieEntity> Movies { get; set; } = new List<MovieEntity>();
}
