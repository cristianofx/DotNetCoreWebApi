using DotNetCoreWebApi.Data;

namespace DotNetCoreWebApi.Infrastructure.Response
{
    public class Collection<T> : Resource
    {
        public T[] Value { get; set; }
    }
}
