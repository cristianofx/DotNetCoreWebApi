using AutoMapper;
using DotNetCoreWebApi.Data;
using DotNetCoreWebApi.Infrastructure.Response;

namespace DotNetCoreWebApi.Infrastructure.Profiles
{
    public class BookingProfile : Profile
    {
        public BookingProfile()
        {
            CreateMap<BookingEntity, Booking>()
                .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total / 100m))
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src =>
                    Link.To(
                        nameof(Controllers.BookingsController.GetBookingById),
                        new { bookingId = src.Id })))
                .ForMember(dest => dest.Room, opt => opt.MapFrom(src =>
                    Link.To(
                        nameof(Controllers.RoomsController.GetRoomById),
                        new { roomId = src.Id })));
        }
    }
}
