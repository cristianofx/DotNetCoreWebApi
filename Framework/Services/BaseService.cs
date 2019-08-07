using AutoMapper;
using AutoMapper.QueryableExtensions;
using DotNetCoreWebApi.Data;
using DotNetCoreWebApi.Framework.Response;
using DotNetCoreWebApi.Framework.ServiceInterfaces;
using DotNetCoreWebApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Framework.Services
{
    public class BaseService<T, TEntity> : IBaseService<T, TEntity>
    {
        private readonly ApiDbContext _context;
        private readonly IConfigurationProvider _mappingConfiguration;

        public BaseService(ApiDbContext context, IConfigurationProvider mappingConfiguration)
        {
            _context = context;
            _mappingConfiguration = mappingConfiguration;
        }

        //public async Task<Room> GetRoomAsync(Guid id)
        //{
        //    var entity = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id);

        //    if (entity == null)
        //    {
        //        return null;
        //    }
        //    var mapper = _mappingConfiguration.CreateMapper();
        //    return mapper.Map<Room>(entity);
        //}

        public async Task<PagedResults<T>> GetAllAsync(PagingOptions pagingOptions, SortOptions<T, TEntity> sortOptions, SearchOptions<T, TEntity> searchOptions)
        {
            IQueryable<TEntity> query =  (IQueryable<TEntity>)_context.GetType().GetMethod("Set").MakeGenericMethod(typeof(TEntity)).Invoke(_context, null);// _context.Set(typeof(TEntity));
            query = searchOptions.Apply(query);
            query = sortOptions.Apply(query);

            var size = await query.CountAsync();

            var items = await query
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value)
                .ProjectTo<T>(_mappingConfiguration)
                .ToArrayAsync();

            return new PagedResults<T>
            {
                Items = items,
                TotalSize = size
            };
        }

        //public Task<T> GetRoomAsync(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<PagedResults<T>> GetRoomsAsync(PagingOptions pagingOptions, SortOptions<T, TEntity> sortOptions, SearchOptions<T, TEntity> searchOptions)
        //{
        //    throw new NotImplementedException();
        //}

        private Room NotFound()
        {
            throw new NotImplementedException();
        }
    }
}
