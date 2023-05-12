using CursoAngular.BOL.Entities;
using NetTopologySuite.Geometries;

namespace CursoAngular.BOL;
public class CinemaEntity : BaseEntity
{
    public string? Name { get; set; }
    public Point? Location { get; set; }

    public virtual ICollection<MovieEntity> Movies { get; set; } = new List<MovieEntity>();
    public virtual ICollection<MovieCinemaEntity> MovieCinemas { get; set; } = new List<MovieCinemaEntity>();
}
