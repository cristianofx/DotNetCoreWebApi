using DotNetCoreWebApi.Infrastructure.JSONResponse;
using System;

namespace DotNetCoreWebApi.Data
{
    public class Opening
    {
        public Link Room { get; set; }

        public DateTimeOffset StartAt { get; set; }

        public DateTimeOffset EndAt { get; set; }

        public decimal Rate { get; set; }
    }
}
