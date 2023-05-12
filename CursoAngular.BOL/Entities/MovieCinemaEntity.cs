namespace CursoAngular.BOL.Entities
{
    public class MovieCinemaEntity : BaseEntity
    {
        public int CinemaId { get; set; }
        public int MovieId { get; set; }

        public virtual CinemaEntity? Cinema { get; set; }
        public virtual MovieEntity? Movie { get; set; }
    }
}
