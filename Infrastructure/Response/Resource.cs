using Newtonsoft.Json;

namespace DotNetCoreWebApi.Infrastructure.Response
{

    public abstract class Resource : Link
    {
        [JsonIgnore]
        public Link Self { get; set; }
    }
}
