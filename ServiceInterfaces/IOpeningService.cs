using DotNetCoreWebApi.Data;
using DotNetCoreWebApi.Framework.Response;
using DotNetCoreWebApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreWebApi.ServiceInterfaces
{
    public interface IOpeningService
    {
        Task<PagedResults<Opening>> GetOpeningsAsync(
            PagingOptions pagingOptions,
            SortOptions<Opening, OpeningEntity> sortOptions,
            SearchOptions<Opening, OpeningEntity> searchOptions);

        Task<IEnumerable<BookingRange>> GetConflictingSlots(
            Guid roomId,
            DateTimeOffset start,
            DateTimeOffset end);
    }
}
