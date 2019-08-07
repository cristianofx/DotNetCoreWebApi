using System.Collections.Generic;

namespace DotNetCoreWebApi.Framework.Response
{
    public class PagedResults<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int TotalSize { get; set; }
    }
}
