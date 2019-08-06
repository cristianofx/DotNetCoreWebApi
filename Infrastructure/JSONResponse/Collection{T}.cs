using DotNetCoreWebApi.Data;

namespace DotNetCoreWebApi.Infrastructure.JSONResponse
{
    public class Collection<T> : Resource
    {
        public T[] Value { get; set; }
    }
}
