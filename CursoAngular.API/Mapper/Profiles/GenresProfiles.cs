using AutoMapper;
using CursoAngular.API.DTO;
using CursoAngular.BOL;

namespace CursoAngular.API.Mapper.Profiles
{
    public class GenresProfiles : Profile
    {
        public GenresProfiles()
        {
            // Entity to DTO
            CreateMap<GenreEntity, GenreDTO>();

            // DTO to Entity
            CreateMap<GenreDTO, GenreEntity>();
        }
    }
}
