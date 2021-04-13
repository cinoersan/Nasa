using AutoMapper;
using Nasa.Model.Movements;

namespace Nasa.Business.Profiles
{
    public class LocationProfile: Profile
    {
        public LocationProfile()
        {
            CreateMap<RoverStatus, Coordinate>()
                .ForMember(dest => dest.X, opt => opt.MapFrom(src => src.CurrentCoordinate.X))
                .ForMember(dest => dest.Y, opt => opt.MapFrom(src => src.CurrentCoordinate.Y));
        }
    }
}
