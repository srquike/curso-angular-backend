namespace CursoAngular.BOL;
public class StarEntity
{
    public int StarId { get; set; }
    public string? Name { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? PhotographyURL { get; set; }

    public virtual ICollection<MovieEntity> Movies { get; set; } = new List<MovieEntity>();
}
