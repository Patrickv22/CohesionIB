using ServiceRequest.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceRequest.Api.Providers
{
    public class ServiceRequestProvider : IServiceRequestProvider
    {
        public Task<(bool success, IEnumerable<Models.ServiceRequest> serviceRequests, string errorMessage)> GetServiceRequestsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<(bool success, Models.ServiceRequest serviceRequest, string errorMessage)> GetServiceRequestAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<(bool success, Models.ServiceRequest serviceRequest, string errorMessage)> CreateServiceRequestAsync(Data.ServiceRequest serviceRequest)
        {
            throw new NotImplementedException();
        }

        public Task<(bool success, Models.ServiceRequest serviceRequest, string errorMessage)> UpdateServiceRequestAsync(Data.ServiceRequest serviceRequest)
        {
            throw new NotImplementedException();
        }

        public Task<(bool success, string errorMessage)> DeleteServiceRequestAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
