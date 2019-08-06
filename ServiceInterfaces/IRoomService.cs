﻿using DotNetCoreWebApi.Data;
using DotNetCoreWebApi.Infrastructure.Response;
using System;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.ServiceInterfaces
{
    public interface IRoomService
    {
        Task<PagedResults<Room>> GetRoomsAsync(PagingOptions pagingOptions, SortOptions<Room, RoomEntity> sortOptions);
        Task<Room> GetRoomAsync(Guid id);
    }
}
