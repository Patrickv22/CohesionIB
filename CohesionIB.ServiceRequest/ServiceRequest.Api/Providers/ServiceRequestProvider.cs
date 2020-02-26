using ServiceRequest.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServiceRequest.Api.Data;

namespace ServiceRequest.Api.Providers
{
    public class ServiceRequestProvider : IServiceRequestProvider
    {
        private readonly ServiceRequestDbContext _context;
        private readonly ILogger<ServiceRequestProvider> _logger;
        private readonly IMapper _mapper;

        public ServiceRequestProvider(ServiceRequestDbContext context, ILogger<ServiceRequestProvider> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            SeedData();
        }

        public async Task<(bool success, IEnumerable<Models.ServiceRequest> serviceRequests, string errorMessage)> GetServiceRequestsAsync()
        {
            try
            {
                var serviceRequests = await _context.ServiceRequests.ToListAsync();
                if (serviceRequests != null && serviceRequests.Any())
                {
                    var result = _mapper.Map<IEnumerable<Data.ServiceRequest>, IEnumerable<Models.ServiceRequest>>(serviceRequests);
                    return (true, result, null);
                }

                return (false, null, "No records found");
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
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

        private void SeedData()
        {
            if (!_context.ServiceRequests.Any())
            {
                var serviceRequests = new[]
                {
                    new Data.ServiceRequest
                    {
                        Id = Guid.NewGuid(), BuildingCode = "COH", Description = "Leaky pipes",
                        CurrentStatus = CurrentStatusEnum.Created, CreatedBy = "John Smith", CreatedDate = DateTime.Now
                    },
                    new Data.ServiceRequest
                    {
                        Id = Guid.NewGuid(), BuildingCode = "BAR", Description = "Heat is not working",
                        CurrentStatus = CurrentStatusEnum.Created, CreatedBy = "Susan Adams", CreatedDate = DateTime.Now
                    },
                    new Data.ServiceRequest
                    {
                        Id = Guid.NewGuid(), BuildingCode = "COH", Description = "Cracks in ceiling",
                        CurrentStatus = CurrentStatusEnum.InProgress, CreatedBy = "John Smith", CreatedDate = DateTime.Now.AddDays(-2)
                    },
                    new Data.ServiceRequest
                    {
                        Id = Guid.NewGuid(), BuildingCode = "BLD", Description = "AC is not working",
                        CurrentStatus = CurrentStatusEnum.Complete, CreatedBy = "Tom Jones", CreatedDate = DateTime.Now.AddDays(-7)
                    },
                    new Data.ServiceRequest
                    {
                        Id = Guid.NewGuid(), BuildingCode = "BLD", Description = "Toilets not working",
                        CurrentStatus = CurrentStatusEnum.Cancelled, CreatedBy = "Tom Jones", CreatedDate = DateTime.Now.AddDays(-3)
                    }
                };

                serviceRequests.ToList().ForEach(x => _context.ServiceRequests.Add(x));
                _context.SaveChanges();
            }
        }
    }
}
