using DotNetCoreWebApi.Data;
using DotNetCoreWebApi.Framework.Forms;
using DotNetCoreWebApi.Framework.Response;
using DotNetCoreWebApi.Framework.ServiceInterfaces;
using DotNetCoreWebApi.Models;
using DotNetCoreWebApi.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Controllers
{
    //[Route("/[controller]")]
    [ApiController]
    public abstract class ReadOnlyController<T, TEntity, TMapping, TResponseModel> : ControllerBase where TResponseModel : PagedCollection<T>, new()
    {
        private readonly IBaseService<T, TEntity> _baseService;
        //private readonly IOpeningService _openingService;
        private readonly PagingOptions _defaultPagingOptions;
        //private readonly IDateLogicService _dateLogicService;
        //private readonly IBookingService _bookingService;

        public ReadOnlyController(IOptions<PagingOptions> defaultPagingOptionsWrapper, IBaseService<T, TEntity> baseService)
        {
            _baseService = baseService;
            //_openingService = openingService;
            _defaultPagingOptions = defaultPagingOptionsWrapper.Value;
            //_dateLogicService = dateLogicService;
            //_bookingService = bookingService;
        }

        // GET /rooms
        [HttpGet(Name = nameof(GetAll))]
        [ProducesResponseType(200)]
        public async Task<ActionResult<Collection<T>>> GetAll(
            [FromQuery] PagingOptions pagingOptions,
            [FromQuery] SortOptions<T, TEntity> sortOptions,
            [FromQuery] SearchOptions<T, TEntity> searchOptions)
        {
            pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
            pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

            var response = await _baseService.GetAllAsync(pagingOptions, sortOptions, searchOptions);

            var collection = PagedCollection<T>.Create<TResponseModel>(
                Link.ToCollection(nameof(GetAll)),
                response.Items.ToArray(),
                response.TotalSize,
                pagingOptions);

            var props = typeof(TResponseModel).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
                                              .Where(X => X.PropertyType == typeof(Form));

            foreach(var prop in props)
            {
                PropertyInfo propertyInfo = collection.GetType().GetProperty(prop.Name);
                propertyInfo.SetValue(collection, 
                                      Convert.ChangeType(FormMetadata.FromResource<T>(Link.ToForm(nameof(GetAll),
                                            null,
                                            Link.GetMethod,
                                            Form.QueryRelation))
                                     , prop.PropertyType)
                                     , null);
            }



            //collection.Openings = Link.ToCollection(nameof(GetAllRoomOpenings));
            //collection.RoomsQuery = FormMetadata.FromResource<Room>(Link.ToForm(nameof(GetAllRooms),
            //                                                        null,
            //                                                        Link.GetMethod,
            //                                                        Form.QueryRelation));

            return collection;
        }

        ////GET /rooms/{roomId}
        //[HttpGet("{roomId}", Name = nameof(GetRoomById))]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(200)]
        //public async Task<ActionResult<Room>> GetRoomById(Guid roomId)
        //{
        //    var room = await _roomService.GetRoomAsync(roomId);

        //    if (room == null)
        //    {
        //        return NotFound();
        //    }

        //    return room;
        //}

        //// GET /rooms/openings
        //[HttpGet("openings", Name = nameof(GetAllRoomOpenings))]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(200)]
        //[ResponseCache(Duration = 30, VaryByQueryKeys = new[] { "offset", "limit", "orderBy", "search" })]
        //public async Task<ActionResult<Collection<Opening>>> GetAllRoomOpenings(
        //    [FromQuery] PagingOptions pagingOptions,
        //    [FromQuery] SortOptions<Opening, OpeningEntity> sortOptions,
        //    [FromQuery] SearchOptions<Opening, OpeningEntity> searchOptions)
        //{
        //    pagingOptions.Offset = pagingOptions.Offset ?? _defaultPagingOptions.Offset;
        //    pagingOptions.Limit = pagingOptions.Limit ?? _defaultPagingOptions.Limit;

        //    var openings = await _openingService.GetOpeningsAsync(
        //        pagingOptions, sortOptions, searchOptions);

        //    var collection = PagedCollection<Opening>.Create<OpeningsResponse>(
        //        Link.ToCollection(nameof(GetAllRoomOpenings)),
        //        openings.Items.ToArray(),
        //        openings.TotalSize,
        //        pagingOptions);

        //    collection.OpeningsQuery = FormMetadata.FromResource<Opening>(
        //        Link.ToForm(
        //            nameof(GetAllRoomOpenings),
        //            null,
        //            Link.GetMethod,
        //            Form.QueryRelation));


        //    return collection;
        //}

        //// POST /rooms/{roomId}/bookings
        ////TODO: authentication
        //[HttpPost("{roomId}/bookings", Name = nameof(CreateBookingForRoomAsync))]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(201)]
        //[ProducesResponseType(400)]
        //public async Task<ActionResult> CreateBookingForRoomAsync(Guid roomId, [FromBody] BookingForm bookingForm)
        //{
        //    var room = await _roomService.GetRoomAsync(roomId);
        //    if (room == null) return NotFound();

        //    var minimumStay = _dateLogicService.GetMinimumStay();
        //    bool tooShort = (bookingForm.EndAt.Value - bookingForm.StartAt.Value) < minimumStay;
        //    if (tooShort) return BadRequest(new ApiError($"The minimum booking duration is {minimumStay.TotalHours} hours"));

        //    var conflictedSlots = await _openingService.GetConflictingSlots(roomId, bookingForm.StartAt.Value, bookingForm.EndAt.Value);
        //    if (conflictedSlots.Any()) return BadRequest(new ApiError($"This time conflict with an existing booking."));

        //    var userId = Guid.NewGuid();

        //    var bookingId = await _bookingService.CreateBookingAsync(userId, roomId, bookingForm.StartAt.Value, bookingForm.EndAt.Value);

        //    return Created(Url.Link(nameof(BookingsController.GetBookingById), new { bookingId }), null);
        //}
    }
}
