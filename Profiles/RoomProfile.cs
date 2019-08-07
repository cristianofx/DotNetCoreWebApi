using AutoMapper;
using DotNetCoreWebApi.Data;
using DotNetCoreWebApi.Framework.Response;

namespace DotNetCoreWebApi.Profiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<RoomEntity, Room>()
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate / 100.0m))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src => Link.To(nameof(Controllers.RoomsController.GetRoomById),
                                                                                  new { roomId = src.Id })));
        }
    }
}
