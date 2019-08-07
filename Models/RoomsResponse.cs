using DotNetCoreWebApi.Data;
using DotNetCoreWebApi.Framework.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Models
{
    public class RoomsResponse : PagedCollection<Room>
    {
        public Link Openings { get; set; }
    }
}
