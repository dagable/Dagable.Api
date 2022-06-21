using Dagable.Api.RequestModels;
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
        public string Generate()
        {
            return _dagServices.CreateDag();
        }

        [HttpPost]
        [Authorize]
        public string Post(GenerateGraphDTO grapDetails)
        {
            return _dagServices.CreateDag(grapDetails);
        }
    }
}
