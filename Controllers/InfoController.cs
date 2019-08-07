using DotNetCoreWebApi.Extensions;
using DotNetCoreWebApi.Framework.Attributes;
using DotNetCoreWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DotNetCoreWebApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly HotelInfo _hotelInfo;

        public InfoController(IOptions<HotelInfo> hotelInfoWrapper)
        {
            _hotelInfo = hotelInfoWrapper.Value;
        }

        [HttpGet(Name = nameof(GetInfo))]
        [ProducesResponseType(200)]
        [ProducesResponseType(304)]
        [ResponseCache(CacheProfileName = "Static")]
        [Etag]
        public ActionResult<HotelInfo> GetInfo()
        {
            _hotelInfo.Href = Url.Link(nameof(GetInfo), null);

            if (!Request.GetEtagHandler().NoneMatch(_hotelInfo))
            {
                return StatusCode(304, _hotelInfo);
            }
            return _hotelInfo;
        }
    }
}
