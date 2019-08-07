using Newtonsoft.Json;

namespace DotNetCoreWebApi.Framework.Response
{

    public abstract class Resource : Link
    {
        [JsonIgnore]
        public Link Self { get; set; }
    }
}
