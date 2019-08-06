using DotNetCoreWebApi.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.ServiceInterfaces
{
    public interface IOpeningService
    {
        Task<IEnumerable<Opening>> GetOpeningsAsync();

        Task<IEnumerable<BookingRange>> GetConflictingSlots(
            Guid roomId,
            DateTimeOffset start,
            DateTimeOffset end);
    }
}
