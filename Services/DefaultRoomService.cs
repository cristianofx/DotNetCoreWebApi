using AutoMapper;
using AutoMapper.QueryableExtensions;
using DotNetCoreWebApi.Data;
using DotNetCoreWebApi.Framework.Providers;
using DotNetCoreWebApi.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.Services
{
    public class DefaultRoomService : IRoomService
    {
        private readonly ApiDbContext _context;
        private readonly IConfigurationProvider _mappingConfiguration;

        public DefaultRoomService(ApiDbContext context, IConfigurationProvider mappingConfiguration)
        {
            _context = context;
            _mappingConfiguration = mappingConfiguration;
        }

        public async Task<Room> GetRoomAsync(Guid id)
        {
            var entity = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return null;
            }
            var mapper = _mappingConfiguration.CreateMapper();
            return mapper.Map<Room>(entity);
        }

        public async Task<PagedResults<Room>> GetRoomsAsync(PagingOptions pagingOptions, SortOptions<Room, RoomEntity> sortOptions, SearchOptions<Room, RoomEntity> searchOptions)
        {
            IQueryable<RoomEntity> query = _context.Rooms;
            query = searchOptions.Apply(query);
            query = sortOptions.Apply(query);

            var size = await query.CountAsync();

            var items = await query
                .Skip(pagingOptions.Offset.Value)
                .Take(pagingOptions.Limit.Value)
                .ProjectTo<Room>(_mappingConfiguration)
                .ToArrayAsync();

            return new PagedResults<Room>
            {
                Items = items,
                TotalSize = size
            };
        }

        private Room NotFound()
        {
            throw new NotImplementedException();
        }
    }
}
