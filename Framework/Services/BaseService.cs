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
    public class BaseService<T, TEntity> : IBaseService<T, TEntity> where TEntity : class
    {
        private readonly ApiDbContext _context;
        private readonly IConfigurationProvider _mappingConfiguration;
        protected string[] idFields => new string[] { "Id" };

        public BaseService(ApiDbContext context, IConfigurationProvider mappingConfiguration)
        {
            _context = context;
            _mappingConfiguration = mappingConfiguration;
        }

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

        public async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await _context.Set<TEntity>().FindAsync(id);

            if (entity == null)
            {
                return default(T);
            }
            var mapper = _mappingConfiguration.CreateMapper();
            return mapper.Map<T>(entity);
        }

        private T NotFound()
        {
            throw new NotImplementedException();
        }
    }
}
