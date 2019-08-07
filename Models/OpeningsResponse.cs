using DotNetCoreWebApi.Framework.Response;

namespace DotNetCoreWebApi.Models
{
    public class OpeningsResponse : PagedCollection<Opening>
    {
        public Form OpeningsQuery { get; set; }
    }
}
