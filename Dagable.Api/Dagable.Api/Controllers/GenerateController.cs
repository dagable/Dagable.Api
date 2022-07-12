using Dagable.Api.Core;
using Dagable.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Dagable.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenerateController : ControllerBase
    {
        private readonly ILogger<GenerateController> _logger;
        private readonly IDagServices _dagServices;

        public GenerateController(ILogger<GenerateController> logger, IDagServices dagServices)
        {
            _logger = logger;
            _dagServices = dagServices;
        }

        [HttpGet]
        [Route("standard")]
        public string Standard()
        {
            return _dagServices.CreateDag();
        }

        [HttpPost]
        [Route("standard")]
        [Authorize]
        public string Standard(GenerateStandardGraphDTO graphDetails)
        {
            return _dagServices.CreateDag(graphDetails);
        }

        [HttpPost]
        [Route("critical-path")]
        [Authorize]
        public string CriticalPath(GenerateCriticalGraphDTO graphDetails)
        {
            return _dagServices.CreateCriticalPathDag(graphDetails);
        }
    }
}
