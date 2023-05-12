using AutoMapper;
using CursoAngular.API.DTO.Stars;
using CursoAngular.API.Extensions;
using CursoAngular.BOL;

namespace CursoAngular.API.Mapper.Profiles
{
    public class StarsProfiles : Profile
    {
        public StarsProfiles()
        {
            // Entity to DTO
            CreateMap<StarEntity, StarDTO>();
            CreateMap<StarEntity, IndexStarDTO>()
                .ForMember(d => d.DateOfBirth, options => options.MapFrom(s => s.DateOfBirth.ToDateString()));

            // DTO to Entity
            CreateMap<StarDTO, StarEntity>();
            CreateMap<FormStarDTO, StarEntity>()
                .ForMember(d => d.PhotographyURL, options => options.Ignore());
        }
    }
}
