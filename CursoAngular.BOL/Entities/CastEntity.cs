namespace CursoAngular.BOL.Entities
{
    public class CastEntity : BaseEntity
    {
        public int StarId { get; set; }
        public int MovieId { get; set; }
        public string? Character { get; set; }
        public int Order { get; set; }

        public virtual MovieEntity? Movie { get; set; }
        public virtual StarEntity? Star { get; set; }
    }
}
