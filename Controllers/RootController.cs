using DotNetCoreWebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebApi.Controllers
{
    [Route("/")]
    [ApiController]
    [ApiVersion("1.0")]
    public class RootController : ControllerBase
    {
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var response = new RootResponse
            {
                Href = null,
                Rooms = Link.To(nameof(RoomsController.GetRooms)), //= Url.Link(nameof(RoomsController.GetRooms), null),
                Info = Link.To(nameof(InfoController.GetInfo))
            };

            //var response = new
            //{
            //    href = Url.Link(nameof(GetRoot), null),
            //    rooms = new
            //    {
            //        href = Url.Link(nameof(RoomsController.GetRooms), null)
            //    },
            //    info = new
            //    {
            //        href = Url.Link(nameof(InfoController.GetInfo), null)
            //    }
            //};

            return Ok(response);
        }
    }
}
