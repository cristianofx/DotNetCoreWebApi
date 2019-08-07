using DotNetCoreWebApi.Framework.Attributes;
using DotNetCoreWebApi.Framework.Response;

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
