using AutoMapper;
using DotNetCoreWebApi.Data;
using DotNetCoreWebApi.Framework.Response;
using DotNetCoreWebApi.Models;

namespace DotNetCoreWebApi.MappingProfiles
{
    public class OpeningProfile : Profile
    {
        public OpeningProfile()
        {
            CreateMap<OpeningEntity, Opening>()
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate / 100m))
                .ForMember(dest => dest.StartAt, opt => opt.MapFrom(src => src.StartAt))
                .ForMember(dest => dest.EndAt, opt => opt.MapFrom(src => src.EndAt))
                .ForMember(dest => dest.Room, opt => opt.MapFrom(src =>
                    Link.To(
                        nameof(Controllers.RoomsController.GetById),
                        new { roomId = src.RoomId })));
        }
    }
}
