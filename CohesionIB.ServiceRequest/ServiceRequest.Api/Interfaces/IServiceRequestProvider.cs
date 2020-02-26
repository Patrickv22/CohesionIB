using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceRequest.Api.Models;

namespace ServiceRequest.Api.Interfaces
{
    public interface IServiceRequestProvider
    {
        Task<(bool success, IEnumerable<Models.ServiceRequest> serviceRequests, string errorMessage)> GetServiceRequestsAsync();
        Task<(bool success, Models.ServiceRequest serviceRequest, string errorMessage)> GetServiceRequestAsync(Guid id);
        Task<(bool success, Models.ServiceRequest serviceRequest, string errorMessage)> CreateServiceRequestAsync(Models.ServiceRequest serviceRequest);
        Task<(ReturnStatusEnum status, Models.ServiceRequest serviceRequest, string errorMessage)> UpdateServiceRequestAsync(Models.ServiceRequest serviceRequest);
        Task<(bool success, string errorMessage)> DeleteServiceRequestAsync(Guid id);
    }
}
