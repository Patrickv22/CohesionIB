using ServiceRequest.Api.Providers;
using System;
using System.Collections.Generic;
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
            Assert.IsAssignableFrom<IEnumerable<Api.Models.ServiceRequest>>(result.serviceRequests);
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

            var result2 = await provider.GetServiceRequestsAsync();
            Assert.True(result2.success);
            Assert.IsAssignableFrom<IEnumerable<Api.Models.ServiceRequest>>(result2.serviceRequests);
            Assert.Null(result2.errorMessage);
        }

        [Fact]
        public async void update_service_request_success()
        {
            var provider = new ServiceRequestProvider(_fixture.DbContext, null, _fixture.Mapper);
            var existingResult = await provider.GetServiceRequestAsync(Guid.Parse("f335f0f0-7575-4f39-8836-d12572c74fbc"));
            var sr = existingResult.serviceRequest;

            sr.CurrentStatus = Api.Models.CurrentStatusEnum.InProgress;
            

            var result = await provider.UpdateServiceRequestAsync(sr);

            Assert.True(result.status == Api.Models.ReturnStatusEnum.Success);
            Assert.NotNull(result.serviceRequest);
            Assert.IsType<Api.Models.ServiceRequest>(result.serviceRequest);
            Assert.Null(result.errorMessage);
            Assert.Equal(Api.Models.CurrentStatusEnum.InProgress, result.serviceRequest.CurrentStatus);

            var result2 = await provider.GetServiceRequestsAsync();
            Assert.True(result2.success);
            Assert.IsAssignableFrom<IEnumerable<Api.Models.ServiceRequest>>(result2.serviceRequests);
            Assert.Null(result2.errorMessage);
        }

        [Fact]
        public async void delete_service_request_success()
        {
            var guid = Guid.Parse("3eece8a4-7828-4675-a61f-116d3e5686d5");

            var provider = new ServiceRequestProvider(_fixture.DbContext, null, _fixture.Mapper);

            var result = await provider.DeleteServiceRequestAsync(guid);

            Assert.True(result.success);
            Assert.Null(result.errorMessage);

        }



    }
}
