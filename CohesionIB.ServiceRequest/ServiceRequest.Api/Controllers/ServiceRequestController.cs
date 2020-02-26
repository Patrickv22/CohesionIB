using Microsoft.AspNetCore.Mvc;
using ServiceRequest.Api.Interfaces;
using System;
using System.Threading.Tasks;

namespace ServiceRequest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceRequestController : ControllerBase
    {
        private readonly IServiceRequestProvider _provider;

        public ServiceRequestController(IServiceRequestProvider provider)
        {
            _provider = provider;
        }

        [HttpGet]
        public async Task<IActionResult> GetServiceRequestsAsync()
        {
            var result = await _provider.GetServiceRequestsAsync();
            if (result.success)
                return Ok(result.serviceRequests);

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceRequestAsync(Guid id)
        {
            var result = await _provider.GetServiceRequestAsync(id);
            if (result.success)
                return Ok(result.serviceRequest);

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServiceRequest(Guid id)
        {
            var result = await _provider.DeleteServiceRequestAsync(id);
            if (result.success)
                return Ok();

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateServiceRequestAsync(Models.ServiceRequest model)
        {
            var result = await _provider.CreateServiceRequestAsync(model);
            if (result.success)
                return Ok(result.serviceRequest);

            return BadRequest(result.errorMessage);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateServiceRequestAsync(Models.ServiceRequest model)
        {
            var result = await _provider.UpdateServiceRequestAsync(model);
            if (result.status == Models.ReturnStatusEnum.Success)
                return Ok(result.serviceRequest);

            if (result.status == Models.ReturnStatusEnum.NotFound)
                return NotFound();

            return BadRequest(result.errorMessage);
        }
    }
}