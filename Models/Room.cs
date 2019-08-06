using DotNetCoreWebApi.Infrastructure.Attributes;
using DotNetCoreWebApi.Infrastructure.Response;

namespace DotNetCoreWebApi.Data
{
    public class Room : Resource
    {
        [Sortable]
        public string Name { get; set; }

        [Sortable(Default = true)]
        public decimal Rate { get; set; }
    }
}
