using DotNetCoreWebApi.Data;
using DotNetCoreWebApi.Framework.Response;
using DotNetCoreWebApi.Models;
using System;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Framework.ServiceInterfaces
{
    public interface IBaseService<T, TEntity>
    {
        Task<PagedResults<T>> GetAllAsync(PagingOptions pagingOptions, SortOptions<T, TEntity> sortOptions, SearchOptions<T, TEntity> searchOptions);
        Task<T> GetByIdAsync(Guid id);
    }
}
