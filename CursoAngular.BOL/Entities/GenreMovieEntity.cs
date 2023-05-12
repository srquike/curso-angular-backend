namespace CursoAngular.BOL.Entities
{
    public class GenreMovieEntity : BaseEntity
    {
        public int GenreId { get; set; }
        public int MovieId { get; set; }

        public virtual GenreEntity? Genre { get; set; }
        public virtual MovieEntity? Movie { get; set; }
    }
}
