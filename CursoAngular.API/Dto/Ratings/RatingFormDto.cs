using System.ComponentModel.DataAnnotations;

namespace CursoAngular.API.Dto.Ratings
{
    public class RatingFormDto
    {
        [Range(1, 5)]
        public int Scoring { get; set; }

        [Required]
        public int MovieId { get; set; }
    }
}
