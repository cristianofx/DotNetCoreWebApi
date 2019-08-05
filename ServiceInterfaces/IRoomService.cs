using DotNetCoreWebApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.ServiceInterfaces
{
    public interface IRoomService
    {
        Task<Room> GetRoomAsync(Guid id);
    }
}
