using Dagable.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Dagable.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class GenerateController : ControllerBase
    {
        private readonly ILogger<GenerateController> _logger;
        private readonly IDagServices _dagServices;

        public GenerateController(ILogger<GenerateController> logger, IDagServices dagServices)
        {
            _logger = logger;
            _dagServices = dagServices;
        }

        [HttpPost]
        public string Post()
        {
            return _dagServices.CreateDag();
        }
    }
}
