using Microsoft.AspNetCore.Identity;

namespace CursoAngular.BOL.Entities
{
    public class RatingEntity : BaseEntity
    {
        public int Scoring { get; set; }
        public int MovieId { get; set; }
        public string? UserId { get; set; }

        public virtual MovieEntity? Movie { get; set; }
        public virtual IdentityUser? User { get; set; }
    }
}
