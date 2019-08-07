using DotNetCoreWebApi.Data;

namespace DotNetCoreWebApi.Framework.Providers
{
    public class Collection<T> : Resource
    {
        public T[] Value { get; set; }
    }
}
