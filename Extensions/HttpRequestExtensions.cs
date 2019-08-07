using DotNetCoreWebApi.Framework.Interfaces;
using Microsoft.AspNetCore.Http;

namespace DotNetCoreWebApi.Extensions
{
    public static class HttpRequestExtensions
    {
        public static IEtagHandlerFeature GetEtagHandler(this HttpRequest request)
            => request.HttpContext.Features.Get<IEtagHandlerFeature>();
    }
}
