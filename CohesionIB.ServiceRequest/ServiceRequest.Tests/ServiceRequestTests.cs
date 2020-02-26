using ServiceRequest.Api.Providers;
using System;
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
            var provider = new ServiceRequestProvider(_fixture.DbContext, null, _fixture.Mapper);

            var result = await provider.GetServiceRequestsAsync();
            Assert.True(result.success);
            Assert.True(result.serviceRequests.Count() == 5);
            Assert.Null(result.errorMessage);
        }

        [Fact]
        public async void return_product_by_id_success()
        {
            var guid = Guid.Parse("f335f0f0-7575-4f39-8836-d12572c74fbc");
            var provider = new ServiceRequestProvider(_fixture.DbContext, null, _fixture.Mapper);

            var result = await provider.GetServiceRequestAsync(guid);
            Assert.True(result.success);
            Assert.NotNull(result.serviceRequest);
            Assert.IsType<Api.Models.ServiceRequest>(result.serviceRequest);
            Assert.Equal(guid, result.serviceRequest.Id);
            Assert.Null(result.errorMessage);
        }

        [Fact]
        public async void return_product_by_id_fail()
        {
            var guid = Guid.NewGuid();
            var provider = new ServiceRequestProvider(_fixture.DbContext, null, _fixture.Mapper);

            var result = await provider.GetServiceRequestAsync(guid);

            Assert.False(result.success);
            Assert.Null(result.serviceRequest);
            Assert.NotNull(result.errorMessage);
        }

        [Fact]
        public async void create_new_service_request_success()
        {
            var provider = new ServiceRequestProvider(_fixture.DbContext, null, _fixture.Mapper);

            var result = await provider.CreateServiceRequestAsync(_fixture.NewServiceRequest);

            Assert.True(result.success);
            Assert.NotNull(result.serviceRequest);
            Assert.IsType<Api.Models.ServiceRequest>(result.serviceRequest);
            Assert.Null(result.errorMessage);

            var results = await provider.GetServiceRequestsAsync();
            Assert.True(results.serviceRequests.Count() == 6);
        }

        


    }
}
