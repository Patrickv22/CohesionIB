using Microsoft.EntityFrameworkCore;
using ServiceRequest.Api.Data;
using System;

namespace ServiceRequest.Tests
{
    public class DbContextFixture : IDisposable
    {
        public ServiceRequestDbContext DbContext { get; set; }

        public DbContextFixture()
        {
            var options = new DbContextOptionsBuilder<ServiceRequestDbContext>()
                .UseInMemoryDatabase(nameof(DbContextFixture))
                .Options;
            DbContext = new ServiceRequestDbContext(options);
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
