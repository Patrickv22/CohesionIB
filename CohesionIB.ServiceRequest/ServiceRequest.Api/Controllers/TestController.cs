using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ServiceRequest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        public ActionResult TestLog()
        {
            _logger.LogDebug($"Log Debug");
            _logger.LogInformation($"Log Information");
            _logger.LogWarning("Log Warning");
            _logger.LogError("Log Error");
            _logger.LogInformation("TestLog Controller Method");
            return Ok();
        }
    }
}