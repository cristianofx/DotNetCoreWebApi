namespace DotNetCoreWebApi.Framework.Interfaces
{
    public interface IEtagHandlerFeature
    {
        bool NoneMatch(IEtaggable entity);
    }
}
