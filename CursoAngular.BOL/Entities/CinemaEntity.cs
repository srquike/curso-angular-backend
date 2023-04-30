namespace CursoAngular.BOL;
public class CinemaEntity
{
    public int CinemaId { get; set; }
    public string? Name { get; set; }
    public string? Location { get; set; }

    public virtual ICollection<MovieEntity> Movies { get; set; } = new List<MovieEntity>();
}
