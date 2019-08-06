using DotNetCoreWebApi.Infrastructure.JSONResponse;

namespace DotNetCoreWebApi.Data
{
    public class Room : Resource
    {
        public string Name { get; set; }
        public decimal Rate { get; set; }
    }
}
