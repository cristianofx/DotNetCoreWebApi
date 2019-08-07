using DotNetCoreWebApi.Data;
using DotNetCoreWebApi.Framework.Providers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.ServiceInterfaces
{
    public interface IOpeningService
    {
        Task<PagedResults<Opening>> GetOpeningsAsync(PagingOptions pagingOptions);

        Task<IEnumerable<BookingRange>> GetConflictingSlots(
            Guid roomId,
            DateTimeOffset start,
            DateTimeOffset end);
    }
}
