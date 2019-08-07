using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DotNetCoreWebApi.Data
{
    public class ApiDbContext : IdentityDbContext<UserEntity, UserRoleEntity, Guid>
    {
        public ApiDbContext(DbContextOptions options)
            : base(options) { }
        
        public DbSet<RoomEntity> Rooms { get; set; }
        public DbSet<BookingEntity> Bookings { get; set; }
    }
}
