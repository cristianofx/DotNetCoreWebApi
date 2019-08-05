using AutoMapper;
using DotNetCoreWebApi.Data;
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
        private readonly IMapper _mapper;

        public DefaultRoomService(ApiDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Room> GetRoomAsync(Guid id)
        {
            var entity = await _context.Rooms.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return null;
            }

            return _mapper.Map<Room>(entity);
        }

        private Room NotFound()
        {
            throw new NotImplementedException();
        }
    }
}
