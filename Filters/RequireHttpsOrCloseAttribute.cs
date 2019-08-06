using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DotNetCoreWebApi.Filters
{
    public class RequireHttpsOrCloseAttribute : RequireHttpsAttribute
    {
        //Changed the way to handle non HTTPS requests to respond with 400 bad request
        protected override void HandleNonHttpsRequest(AuthorizationFilterContext filterContext)
        {
            filterContext.Result = new StatusCodeResult(400);
        }
    }
}
