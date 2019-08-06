using AutoMapper;
using AutoMapper.QueryableExtensions;
using DotNetCoreWebApi.Data;
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

        public async Task<IEnumerable<Room>> GetRoomsAsync()
        {
            var query = _context.Rooms.ProjectTo<Room>(_mappingConfiguration);

            return await query.ToArrayAsync();
        }

        private Room NotFound()
        {
            throw new NotImplementedException();
        }
    }
}
