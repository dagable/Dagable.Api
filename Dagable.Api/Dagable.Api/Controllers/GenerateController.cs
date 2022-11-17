using Dagable.Api.Core;
using Dagable.Api.Services;
using Dagable.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dagable.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenerateController : ControllerBase
    {
        private readonly ILogger<GenerateController> _logger;
        private readonly IDagGenerationServices _dagServices;
        private readonly IDagScheduleServices _dagScheduleServices;

        public GenerateController(ILogger<GenerateController> logger, IDagGenerationServices dagServices, IDagScheduleServices dagScheduleServices)
        {
            _logger = logger;
            _dagServices = dagServices;
            _dagScheduleServices = dagScheduleServices;
        }

        [HttpGet]
        [Route("standard")]
        public JsonResult Standard()
        {
            var graph = _dagServices.CreateDag();
            return new JsonResult(new
            {
                graph,
            });
        }

        [HttpPost]
        [Route("standard")]
        [Authorize]
        public JsonResult Standard(GenerateStandardGraphDTO graphDetails)
        {
            return new JsonResult(new
            {
                graph = _dagServices.CreateDag(graphDetails),
            });
        }

        [HttpPost]
        [Route("critical-path")]
        [Authorize]
        public JsonResult CriticalPath(GenerateCriticalGraphDTO graphDetails)
        {
            var graph = _dagServices.CreateCriticalPathDag(graphDetails);
            var scheduled = _dagScheduleServices.CreateSchedule(graphDetails.Processors, graph);
            return new JsonResult(new
            {
                graph,
                schedule = scheduled
            });
        }
    }
}
