using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceRequest.Api.Interfaces
{
    public interface IServiceRequestProvider
    {
        Task<(bool success, IEnumerable<Models.ServiceRequest> serviceRequests, string errorMessage)> GetServiceRequestsAsync();
        Task<(bool success, Models.ServiceRequest serviceRequest, string errorMessage)> GetServiceRequestAsync(Guid id);
        Task<(bool success, Models.ServiceRequest serviceRequest, string errorMessage)> CreateServiceRequestAsync(Data.ServiceRequest serviceRequest);
        Task<(bool success, Models.ServiceRequest serviceRequest, string errorMessage)> UpdateServiceRequestAsync(Data.ServiceRequest serviceRequest);
        Task<(bool success, string errorMessage)> DeleteServiceRequestAsync(Guid id);
    }
}
