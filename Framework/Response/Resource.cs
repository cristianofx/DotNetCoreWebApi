using Newtonsoft.Json;

namespace DotNetCoreWebApi.Framework.Providers
{

    public abstract class Resource : Link
    {
        [JsonIgnore]
        public Link Self { get; set; }
    }
}
