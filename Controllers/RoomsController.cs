using DotNetCoreWebApi.Data;
using DotNetCoreWebApi.Framework.Forms;
using DotNetCoreWebApi.Framework.Response;
using DotNetCoreWebApi.Framework.ServiceInterfaces;
using DotNetCoreWebApi.MappingProfiles;
using DotNetCoreWebApi.Models;
using DotNetCoreWebApi.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class RoomsController : ReadOnlyController<Room, RoomEntity, RoomProfile, RoomsResponse> //ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IOpeningService _openingService;
        private readonly PagingOptions _defaultPagingOptions;
        private readonly IDateLogicService _dateLogicService;
        private readonly IBookingService _bookingService;

        protected override Dictionary<string, Link> relations => new Dictionary<string, Link>() { ["Openings"] = Link.ToCollection(nameof(GetAllRoomOpenings)) };

        public RoomsController(IBaseService<Room, RoomEntity> baseService, IRoomService roomService, IOpeningService openingService, IOptions<PagingOptions> defaultPagingOptionsWrapper,
            IDateLogicService dateLogicService, IBookingService bookingService) : base(defaultPagingOptionsWrapper, baseService)
        {
            _roomService = roomService;
            _openingService = openingService;
            _defaultPagingOptions = defaultPagingOptionsWrapper.Value;
            _dateLogicService = dateLogicService;
            _bookingService = bookingService;
        }

        // GET /rooms/openings
        [HttpGet("openings", Name = nameof(GetAllRoomOpenings))]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        [ResponseCache(Duration = 30, VaryByQueryKeys = new[] { "offset", "limit", "orderBy", "search"})]
        public async Task<ActionResult<Collection<Opening>>> GetAllRoomOpenings(
            [FromQuery] PagingOptions pagingOptions,
            [FromQuery] SortOptions<Opening, OpeningEntity> sortOptions,
            [FromQuery] SearchOptions<Opening, OpeningEntity> searchOptions)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var openings = await _openingService.GetOpeningsAsync(
                pagingOptions, sortOptions, searchOptions);

            var collection = PagedCollection<Opening>.Create<OpeningsResponse>(
                Link.ToCollection(nameof(GetAllRoomOpenings)),
                openings.Items.ToArray(),
                openings.TotalSize,
                pagingOptions);

            collection.OpeningsQuery = FormMetadata.FromResource<Opening>(
                Link.ToForm(
                    nameof(GetAllRoomOpenings),
                    null,
                    Link.GetMethod,
                    Form.QueryRelation));


            return collection;
        }

        // POST /rooms/{roomId}/bookings
        //TODO: authentication
        [HttpPost ("{roomId}/bookings", Name = nameof(CreateBookingForRoomAsync))]
        [ProducesResponseType(404)]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateBookingForRoomAsync(Guid roomId, [FromBody] BookingForm bookingForm)
        {
            var room = await _roomService.GetRoomAsync(roomId);
            if (room == null) return NotFound();

            var minimumStay = _dateLogicService.GetMinimumStay();
            bool tooShort = (bookingForm.EndAt.Value - bookingForm.StartAt.Value) < minimumStay;
            if (tooShort) return BadRequest(new ApiError($"The minimum booking duration is {minimumStay.TotalHours} hours"));

            var conflictedSlots = await _openingService.GetConflictingSlots(roomId, bookingForm.StartAt.Value, bookingForm.EndAt.Value);
            if(conflictedSlots.Any()) return BadRequest(new ApiError($"This time conflict with an existing booking."));

            var userId = Guid.NewGuid();

            var bookingId = await _bookingService.CreateBookingAsync(userId, roomId, bookingForm.StartAt.Value, bookingForm.EndAt.Value);

            return Created(Url.Link(nameof(BookingsController.GetBookingById), new { bookingId }), null);
        }
    }
}
