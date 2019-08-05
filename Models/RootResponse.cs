using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Models
{
    public class RootResponse
    {
        public object Href { get; set; }
        public Link Rooms { get; set; }
        public Link Info { get; set; }
    }
}
