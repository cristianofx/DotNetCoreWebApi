using AutoMapper;
using DotNetCoreWebApi.Data;
using DotNetCoreWebApi.Framework.Response;
using DotNetCoreWebApi.Models;

namespace DotNetCoreWebApi.MappingProfiles
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<RoomEntity, Room>()
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate / 100.0m))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src => Link.To(nameof(Controllers.RoomsController.GetRoomById),
                                                                                  new { roomId = src.Id })))
                .ForMember(dest => dest.Book, opt => 
                                              opt.MapFrom(src => 
                                                    FormMetadata.FromModel(new BookingForm(), 
                                                    Link.ToForm(nameof(Controllers.RoomsController.CreateBookingForRoomAsync), 
                                                    new { roomId = src.Id },
                                                    Link.PostMethod,
                                                    Form.CreateRelation))));
        }
    }
}
