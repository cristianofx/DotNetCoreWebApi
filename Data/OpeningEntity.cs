using DotNetCoreWebApi.Models;
using System;

namespace DotNetCoreWebApi.Data
{
    public class OpeningEntity : BookingRange
    {
        public Guid RoomId { get; set; }

        public int Rate { get; set; }
    }
}
