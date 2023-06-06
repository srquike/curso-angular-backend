using CursoAngular.API.Bindings;
using CursoAngular.API.DTO.StarsMovies;
using Microsoft.AspNetCore.Mvc;

namespace CursoAngular.API.DTO.Movies
{
    public class FormMovieDTO
    {
        public string? Title { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public IFormFile? PosterFile { get; set; }
        public string? TrailerUrl { get; set; }
        public string? MpaaRating { get; set; }

        [ModelBinder(BinderType = typeof(CustomModelBinder<List<FormStarsMoviesDTO>?>))]
        public List<FormStarsMoviesDTO>? Casting { get; set; }

        [ModelBinder(BinderType = typeof(CustomModelBinder<List<int>?>))]
        public List<int>? Genres { get; set; }

        [ModelBinder(BinderType = typeof(CustomModelBinder<List<int>?>))]
        public List<int>? Cinemas { get; set; }
    }
}
