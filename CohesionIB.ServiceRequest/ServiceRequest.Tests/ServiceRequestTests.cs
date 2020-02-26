using AutoMapper;
using ServiceRequest.Api.Profiles;
using ServiceRequest.Api.Providers;
using System.Linq;
using Xunit;

namespace ServiceRequest.Tests
{

    public class ServiceRequestTests : IClassFixture<DbContextFixture>
    {
        private readonly DbContextFixture _fixture;

        public ServiceRequestTests(DbContextFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async void return_all_products_success()
        {
            var profile = new ServiceRequestProfile();
            var mapperConfig = new MapperConfiguration(config => config.AddProfile(profile));
            var mapper = new Mapper(mapperConfig);
            var provider = new ServiceRequestProvider(_fixture.DbContext, null, mapper);

            var result = await provider.GetServiceRequestsAsync();
            Assert.True(result.success);
            Assert.True(result.serviceRequests.Count() == 5);
            Assert.Null(result.errorMessage);
        }


    }
}
