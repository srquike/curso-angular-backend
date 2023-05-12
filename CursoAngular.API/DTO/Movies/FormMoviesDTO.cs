using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using CursoAngular.API.Bindings;
using CursoAngular.API.DTO.StarsMovies;

namespace CursoAngular.API.DTO.Movies
{
    public class FormMoviesDTO
    {
        [DisplayName("Título")]
        [Required(ErrorMessage = "El {0} de la movie es requerido")]
        [StringLength(maximumLength: 10, ErrorMessage = "El {0} de la movie debe tener máximo 10 caracteres")]
        public string? Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public IFormFile? PosterFile { get; set; }
        public string? TrailerUrl { get; set; }
        public string? MpaaRating { get; set; }

        [ModelBinder(BinderType = typeof(CustomModelBinder<List<FormStarsMoviesDTO>?>))]
        public List<FormStarsMoviesDTO>? Cast { get; set; }

        [ModelBinder(BinderType = typeof(CustomModelBinder<int[]?>))]
        public int[]? Genres { get; set; }

        [ModelBinder(BinderType = typeof(CustomModelBinder<int[]?>))]
        public int[]? Cinemas { get; set; }
    }
}
