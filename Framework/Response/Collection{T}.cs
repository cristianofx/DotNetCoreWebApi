namespace DotNetCoreWebApi.Framework.Response
{
    public class Collection<T> : Resource
    {
        public T[] Value { get; set; }
    }
}
