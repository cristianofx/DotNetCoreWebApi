using DotNetCoreWebApi.Infrastructure.Attributes;
using DotNetCoreWebApi.Infrastructure.Response;

namespace DotNetCoreWebApi.Data
{
    public class Room : Resource
    {
        [Sortable]
        [Searchable]
        public string Name { get; set; }

        [Sortable(Default = true)]
        [SearchableDecimal]
        public decimal Rate { get; set; }
    }
}
