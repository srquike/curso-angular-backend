using AutoMapper;
using CursoAngular.API.DTO.Movies;
using CursoAngular.API.Extensions;
using CursoAngular.BOL;
using CursoAngular.BOL.Entities;

namespace CursoAngular.API.Mapper.Profiles
{
    public class MoviesProfiles : Profile
    {
        public MoviesProfiles()
        {
            // Entity to DTO
            CreateMap<MovieEntity, MovieDTO>()
                .ForMember(d => d.ReleaseDate, options => options.MapFrom(s => s.ReleaseDate.ToDateString()));

            // DTO to Entity
            CreateMap<MovieDTO, MovieEntity>();
            CreateMap<FormMoviesDTO, MovieEntity>()
                .ForMember(d => d.PosterUrl, options => options.Ignore())
                .ForMember(d => d.StarMovies, options => options.MapFrom(GetStarMovieMapping))
                .ForMember(d => d.GenreMovies, options => options.MapFrom(GetGenreMovieMapping))
                .ForMember(d => d.CinemaMovies, options => options.MapFrom(GetCinemaMovieMapping));
        }

        private List<StarMovieEntity> GetStarMovieMapping(FormMoviesDTO dTO, MovieEntity entity)
        {
            var results = new List<StarMovieEntity>();

            if (dTO.Cast != null)
            {
                foreach (var item in dTO.Cast)
                {
                    results.Add(new StarMovieEntity()
                    {
                        Character = item.Character,
                        Order = item.Order,
                        StarId = item.StarId
                    });
                }
            }

            return results;
        }

        private List<MovieCinemaEntity> GetCinemaMovieMapping(FormMoviesDTO dTO, MovieEntity entity)
        {
            var results = new List<MovieCinemaEntity>();

            if (dTO.Cinemas != null)
            {
                foreach (var cinemaId in dTO.Cinemas)
                {
                    results.Add(new MovieCinemaEntity()
                    {
                        CinemaId = cinemaId
                    });
                }
            }

            return results;
        }

        private List<GenreMovieEntity> GetGenreMovieMapping(FormMoviesDTO dTO, MovieEntity entity)
        {
            var results = new List<GenreMovieEntity>();

            if (dTO.Genres != null)
            {
                foreach (var genreId in dTO.Genres)
                {
                    results.Add(new GenreMovieEntity()
                    {
                        GenreId = genreId
                    });
                }
            }

            return results;
        }
    }
}
