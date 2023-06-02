using AutoMapper;
using CursoAngular.API.DTO;
using CursoAngular.API.DTO.Cinemas;
using CursoAngular.API.DTO.Movies;
using CursoAngular.API.DTO.StarsMovies;
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
                .ForMember(d => d.Genres, options => options.MapFrom(GetGenresMapping))
                .ForMember(d => d.Cast, options => options.MapFrom(GetCastMapping))
                .ForMember(d => d.Cinemas, options => options.MapFrom(GetCinemasMapping));

            // DTO to Entity
            CreateMap<MovieDTO, MovieEntity>();
            CreateMap<FormDTO, MovieEntity>()
                .ForMember(d => d.PosterUrl, options => options.Ignore())
                .ForMember(d => d.Casting, options => options.MapFrom(GetStarMovieMapping))
                .ForMember(d => d.GenreMovies, options => options.MapFrom(GetGenreMovieMapping))
                .ForMember(d => d.CinemaMovies, options => options.MapFrom(GetCinemaMovieMapping));
        }

        private List<CinemaDTO> GetCinemasMapping(MovieEntity entity, MovieDTO dTO)
        {
            var results = new List<CinemaDTO>();

            if (entity.CinemaMovies != null)
            {
                foreach (var cinema in entity.CinemaMovies)
                {
                    results.Add(new CinemaDTO() { Id = cinema.Cinema.Id, Name = cinema.Cinema.Name, Latitude = cinema.Cinema.Location.Y, Longitude = cinema.Cinema.Location.X });
                }
            }

            return results;
        }

        private List<CastDTO> GetCastMapping(MovieEntity entity, MovieDTO dTO)
        {
            var results = new List<CastDTO>();

            if (entity.Casting != null)
            {
                foreach (var cast in entity.Casting)
                {
                    results.Add(new CastDTO() { Character = cast.Character, Order = cast.Order, StarId = cast.StarId, StarName = cast.Star.Name, PhotographyUrl = cast.Star.PhotographyURL });
                }
            }

            return results;
        }

        private List<GenreDTO> GetGenresMapping(MovieEntity entity, MovieDTO dTO)
        {
            var results = new List<GenreDTO>();

            if (entity.GenreMovies != null)
            {
                foreach (var genre in entity.GenreMovies)
                {
                    results.Add(new GenreDTO() { Id = genre.Genre.Id, Name = genre.Genre.Name });
                }
            }

            return results;
        }

        private List<CastEntity> GetStarMovieMapping(FormDTO dTO, MovieEntity entity)
        {
            var results = new List<CastEntity>();

            if (dTO.Cast != null)
            {
                foreach (var item in dTO.Cast)
                {
                    results.Add(new CastEntity()
                    {
                        Character = item.Character,
                        Order = item.Order,
                        StarId = item.StarId
                    });
                }
            }

            return results;
        }

        private List<MovieCinemaEntity> GetCinemaMovieMapping(FormDTO dTO, MovieEntity entity)
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

        private List<GenreMovieEntity> GetGenreMovieMapping(FormDTO dTO, MovieEntity entity)
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
