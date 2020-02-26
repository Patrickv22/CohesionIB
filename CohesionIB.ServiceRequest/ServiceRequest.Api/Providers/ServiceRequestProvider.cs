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

        public async Task<(bool success, Models.ServiceRequest serviceRequest, string errorMessage)> GetServiceRequestAsync(Guid id)
        {
            try
            {
                var serviceRequest = await _context.ServiceRequests.FirstOrDefaultAsync(x => x.Id == id);
                if (serviceRequest != null)
                {
                    var result = _mapper.Map<Data.ServiceRequest, Models.ServiceRequest>(serviceRequest);
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

        public async Task<(bool success, Models.ServiceRequest serviceRequest, string errorMessage)> CreateServiceRequestAsync(Models.ServiceRequest serviceRequest)
        {
            try
            {
                //assign a new guid
                serviceRequest.Id = Guid.NewGuid();

                //perform validation - skipping for now :(

                //convert to data model
                var dataModel = _mapper.Map<Models.ServiceRequest, Data.ServiceRequest>(serviceRequest);

                //add entity
                await _context.AddAsync(dataModel);
                _context.SaveChanges();

                //convert to view model
                var viewModel = _mapper.Map<Data.ServiceRequest, Models.ServiceRequest>(dataModel);
                
                //return tuple
                return (true, viewModel, null);

            }
            catch (Exception ex)
            {
                _logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public Task<(bool success, Models.ServiceRequest serviceRequest, string errorMessage)> UpdateServiceRequestAsync(Models.ServiceRequest serviceRequest)
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
                        Id = Guid.Parse("f335f0f0-7575-4f39-8836-d12572c74fbc"), BuildingCode = "COH", Description = "Leaky pipes",
                        CurrentStatus = CurrentStatusEnum.Created, CreatedBy = "John Smith", CreatedDate = DateTime.Now
                    },
                    new Data.ServiceRequest
                    {
                        Id = Guid.Parse("d14cc6d0-de43-47f8-af95-e2042b4406a6"), BuildingCode = "BAR", Description = "Heat is not working",
                        CurrentStatus = CurrentStatusEnum.Created, CreatedBy = "Susan Adams", CreatedDate = DateTime.Now
                    },
                    new Data.ServiceRequest
                    {
                        Id = Guid.Parse("8a59c7a6-2a59-4bdd-beb6-70f61d762db1"), BuildingCode = "COH", Description = "Cracks in ceiling",
                        CurrentStatus = CurrentStatusEnum.InProgress, CreatedBy = "John Smith", CreatedDate = DateTime.Now.AddDays(-2)
                    },
                    new Data.ServiceRequest
                    {
                        Id = Guid.Parse("faac06dd-4bf6-449a-ab1d-0a98c90bbc39"), BuildingCode = "BLD", Description = "AC is not working",
                        CurrentStatus = CurrentStatusEnum.Complete, CreatedBy = "Tom Jones", CreatedDate = DateTime.Now.AddDays(-7)
                    },
                    new Data.ServiceRequest
                    {
                        Id = Guid.Parse("3eece8a4-7828-4675-a61f-116d3e5686d5"), BuildingCode = "BLD", Description = "Toilets not working",
                        CurrentStatus = CurrentStatusEnum.Cancelled, CreatedBy = "Tom Jones", CreatedDate = DateTime.Now.AddDays(-3)
                    }
                };

                serviceRequests.ToList().ForEach(x => _context.ServiceRequests.Add(x));
                _context.SaveChanges();
            }
        }
    }
}
