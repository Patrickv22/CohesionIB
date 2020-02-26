using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ServiceRequest.Api.Data;
using ServiceRequest.Api.Profiles;
using System;

namespace ServiceRequest.Tests
{
    public class DbContextFixture : IDisposable
    {
        public ServiceRequestDbContext DbContext { get; set; }
        public Mapper Mapper { get; set; }
        public Api.Models.ServiceRequest NewServiceRequest { get; set; }
        public Guid NewGuid { get; set; } = Guid.NewGuid();

        public DbContextFixture()
        {
            var options = new DbContextOptionsBuilder<ServiceRequestDbContext>()
                .UseInMemoryDatabase(nameof(DbContextFixture))
                .Options;
            DbContext = new ServiceRequestDbContext(options);

            var profile = new ServiceRequestProfile();
            var mapperConfig = new MapperConfiguration(config => config.AddProfile(profile));
            Mapper = new Mapper(mapperConfig);

            NewServiceRequest = new Api.Models.ServiceRequest
            {
                BuildingCode = "AAA",
                Description = "Broken Glass",
                CurrentStatus = Api.Models.CurrentStatusEnum.Created,
                CreatedBy = "Pete Johnson",
                CreatedDate = DateTime.Now
            };
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }
    }
}
