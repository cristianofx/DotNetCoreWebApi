//TODO: Add generic controller for GET responses
//using DotNetCoreWebApi.Infrastructure.JSONResponse;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace DotNetCoreWebApi.Controllers
//{
//    public class ReadOnlyController<T> : ControllerBase
//    {
//        [HttpGet(Name = nameof(GetAll))]
//        [ProducesResponseType(200)]
//        [ProducesResponseType(400)]
//        public async Task<ActionResult<Collection<T>>> GetAll()
//        {
//            var response = await _roomService.GetRoomsAsync();

//            var collection = new Collection<Room>
//            {
//                Self = Link.ToCollection(nameof(GetAllRooms)),
//                Value = rooms.ToArray()
//            };

//            return collection;
//        }
//    }
//}
