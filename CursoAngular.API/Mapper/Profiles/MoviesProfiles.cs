using AutoMapper;
using CursoAngular.API.Extensions;
using CursoAngular.BOL;
using CursoAngular.BOL.DTOs;

namespace CursoAngular.API.Mapper.Profiles
{
    public class MoviesProfiles : Profile
    {
        public MoviesProfiles()
        {
            // Entity to DTO
            CreateMap<MovieEntity, MovieDTO>()
                .ForMember(d => d.ReleaseDate, options => options.MapFrom(s => s.ReleaseDate.ToDateTimeString()));

            // DTO to Entity
            CreateMap<MovieDTO, MovieEntity>();
        }
    }
}
