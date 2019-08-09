using DotNetCoreWebApi.Framework.Attributes;
using DotNetCoreWebApi.Framework.Forms;
using DotNetCoreWebApi.Framework.Response;

namespace DotNetCoreWebApi.Models
{
    public class Room : Resource
    {
        [Sortable]
        [Searchable]
        public string Name { get; set; }

        [Sortable(Default = true)]
        [SearchableDecimal]
        public decimal Rate { get; set; }

        public Form Book { get; set; }
    }
}
