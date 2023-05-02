using CursoAngular.BOL.Entities;

namespace CursoAngular.BOL;
public class CinemaEntity : BaseEntity
{
    public string? Name { get; set; }
    public string? Location { get; set; }

    public virtual ICollection<MovieEntity> Movies { get; set; } = new List<MovieEntity>();
}
