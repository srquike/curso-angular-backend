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
            CreateMap<StarEntity, StarDTO>()
                .ForMember(d => d.Photography, options => options.MapFrom(s => s.PhotographyURL));
            CreateMap<StarEntity, IndexStarDTO>()
                .ForMember(d => d.DateOfBirth, options => options.MapFrom(s => s.DateOfBirth.ToShortDateString()));
            CreateMap<StarEntity, SearchStarDTO>()
                .ForMember(d => d.PhotographyUrl, options => options.MapFrom(s => s.PhotographyURL));

            // DTO to Entity
            CreateMap<StarDTO, StarEntity>();
            CreateMap<FormStarDTO, StarEntity>()
                .ForMember(d => d.PhotographyURL, options => options.Ignore());
        }
    }
}
