using DotNetCoreWebApi.Data;
using DotNetCoreWebApi.Framework.Response;
using DotNetCoreWebApi.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Services
{
    public interface IUserService
    {
        Task<PagedResults<User>> GetUsersAsync(
            PagingOptions pagingOptions,
            SortOptions<User, UserEntity> sortOptions,
            SearchOptions<User, UserEntity> searchOptions);

        Task<(bool Succeded, string ErrorMessage)> CreateUserAsync(RegisterForm form);

        Task<User> GetUserAsync(ClaimsPrincipal user);

        Task<Guid?> GetUserIdAsync(ClaimsPrincipal principal);
    }
}
