using AspNet.Security.OpenIdConnect.Primitives;
using Newtonsoft.Json;

namespace DotNetCoreWebApi.Framework.Response
{
    public class UserinfoResponse : Resource
    {
        [JsonProperty(PropertyName = OpenIdConnectConstants.Claims.Subject)]
        public string Subject { get; set; }

        [JsonProperty(PropertyName = OpenIdConnectConstants.Claims.GivenName)]
        public string GivenName { get; set; }

        [JsonProperty(PropertyName = OpenIdConnectConstants.Claims.FamilyName)]
        public string FamilyName { get; set; }
    }
}
