namespace CursoAngular.BOL;
public class StarEntity
{
    public int ActorId { get; set; }
    public string? Name { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string? Character { get; set; }
    public string? PhotographyURL { get; set; }

    public virtual ICollection<MovieEntity> Movies { get; set; } = new List<MovieEntity>();
}
