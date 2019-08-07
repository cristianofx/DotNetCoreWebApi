using DotNetCoreWebApi.Data;
using DotNetCoreWebApi.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // TODO: authorization
        [HttpGet("{bookingId}", Name = nameof(GetBookingById))]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Booking>> GetBookingById(Guid bookingId)
        {
            var booking = await _bookingService.GetBookingAsync(bookingId);
            if (booking == null) return NotFound();

            return booking;
        }

        // TODO: authorization
        // DELETE /bookings/{bookingId}
        [HttpDelete("{bookingId}", Name = nameof(DeleteBookingById))]
        [ProducesResponseType(204)]
        public async Task<ActionResult<Booking>> DeleteBookingById(Guid bookingId)
        {
            //TODO: authorize that the user is allowed to do this
            await _bookingService.DeleteBookingAsync(bookingId);
            return NoContent();
        }
    }

}
