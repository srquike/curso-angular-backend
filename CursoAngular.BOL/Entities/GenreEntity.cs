using CursoAngular.BOL.Entities;

namespace CursoAngular.BOL;
public class GenreEntity : BaseEntity
{
    public string? Name { get; set; }

    public virtual ICollection<MovieEntity> Movies { get; set; } = new List<MovieEntity>();
}
