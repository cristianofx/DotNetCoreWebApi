using AutoMapper;
using DotNetCoreWebApi.Data;

namespace DotNetCoreWebApi.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RoomEntity, Room>()
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate / 10.0m));
            // TODO Url.Link
        }
    }
}
