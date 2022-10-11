using Dagable.Api.Core;
using Dagable.Api.Services;
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
        private readonly IDagServices _dagServices;
        private readonly IDagScheduleServices _dagScheduleServices;

        public GenerateController(ILogger<GenerateController> logger, IDagServices dagServices, IDagScheduleServices dagScheduleServices)
        {
            _logger = logger;
            _dagServices = dagServices;
            _dagScheduleServices = dagScheduleServices;
        }

        [HttpGet]
        [Route("standard")]
        public string Standard()
        {
            return _dagServices.CreateDag();
        }

        [HttpGet]
        [Route("standardSchedule")]
        public JsonResult StandardSchedule()
        {
            return new JsonResult(_dagScheduleServices.CreateSchedule(3), new JsonSerializerOptions()
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
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
