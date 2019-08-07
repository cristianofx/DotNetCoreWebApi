using AutoMapper;
using DotNetCoreWebApi.Data;
using DotNetCoreWebApi.Framework.Response;

namespace DotNetCoreWebApi.Profiles
{
    public class OpeningProfile : Profile
    {
        public OpeningProfile()
        {
            CreateMap<OpeningEntity, Opening>()
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate / 100m))
                .ForMember(dest => dest.StartAt, opt => opt.MapFrom(src => src.StartAt.UtcDateTime))
                .ForMember(dest => dest.EndAt, opt => opt.MapFrom(src => src.EndAt.UtcDateTime))
                .ForMember(dest => dest.Room, opt => opt.MapFrom(src =>
                    Link.To(
                        nameof(Controllers.RoomsController.GetRoomById),
                        new { roomId = src.RoomId })));
        }
    }
}
