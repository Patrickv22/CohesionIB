using Microsoft.EntityFrameworkCore;
using ServiceRequest.Api.Data;
using System;
using AutoMapper;
using ServiceRequest.Api.Profiles;

namespace ServiceRequest.Tests
{
    public class DbContextFixture : IDisposable
    {
        public ServiceRequestDbContext DbContext { get; set; }
        public Mapper Mapper { get; set; }

        public DbContextFixture()
        {
            var options = new DbContextOptionsBuilder<ServiceRequestDbContext>()
                .UseInMemoryDatabase(nameof(DbContextFixture))
                .Options;
            DbContext = new ServiceRequestDbContext(options);

            var profile = new ServiceRequestProfile();
            var mapperConfig = new MapperConfiguration(config => config.AddProfile(profile));
            Mapper = new Mapper(mapperConfig);
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
