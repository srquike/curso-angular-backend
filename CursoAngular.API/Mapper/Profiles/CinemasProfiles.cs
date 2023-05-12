using AutoMapper;
using CursoAngular.API.DTO.Cinemas;
using CursoAngular.BOL;
using NetTopologySuite.Geometries;

namespace CursoAngular.API.Mapper.Profiles
{
    public class CinemasProfiles : Profile
    {
        public CinemasProfiles(GeometryFactory geometry)
        {
            // Entity to DTO
            CreateMap<CinemaEntity, FormCinemasDTO>()
                .ForMember(d => d.Latitude, options => options.MapFrom(s => s.Location.Y))
                .ForMember(d => d.Longitude, options => options.MapFrom(s => s.Location.X));

            CreateMap<CinemaEntity, IndexCinemasDTO>();

            // DTO to Entity
            CreateMap<FormCinemasDTO, CinemaEntity>()
                .ForMember(d => d.Location, options => options.MapFrom(s => geometry.CreatePoint(new Coordinate(s.Longitude, s.Latitude))));
        }
    }
}
