namespace DotNetCoreWebApi.Infrastructure.Response
{
    public class RootResponse : Resource
    {
        public Link Rooms { get; set; }
        public Link Info { get; set; }
    }
}
