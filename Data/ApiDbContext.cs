using DotNetCoreWebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreWebApi.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options)
            : base(options) { }
        
        public DbSet<RoomEntity> Rooms { get; set; }
    }
}
