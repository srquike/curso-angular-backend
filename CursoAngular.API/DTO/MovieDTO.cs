namespace CursoAngular.API.DTO
{
    public class MovieDTO
    {
        public int Id { get; set; }

        [DisplayName("Título")]
        [Required(ErrorMessage = "El {0} de la movie es requerido")]
        [StringLength(maximumLength: 10, ErrorMessage = "El {0} de la movie debe tener máximo 10 caracteres")]
        public string? Title { get; set; }
        public string? ReleaseDate { get; set; }
        public string? PosterUrl { get; set; }
        public string? TrailerUrl { get; set; }
        public string? MpaaRating { get; set; }
    }
}
