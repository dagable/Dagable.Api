using Dagable.Api.Core;
using Dagable.Api.Core.Graph;
using Dagable.Api.Services;
using Dagable.Api.Services.Graphs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Dagable.Api.Controllers
{
    [Route("[controller]"), ApiController]
    public class GraphsController : ControllerBase
    {
        private readonly ILogger<GraphsController> _logger;
        private readonly IDagScheduleServices _dagScheduleServices;
        private readonly IDagGenerationServices _dagServices;
        private readonly IGraphServices _graphServices;

        public GraphsController(ILogger<GraphsController> logger, IDagGenerationServices dagServices, IDagScheduleServices dagScheduleServices, IGraphServices graphServices)
        {
            _logger = logger;
            _dagServices = dagServices;
            _dagScheduleServices = dagScheduleServices;
            _graphServices = graphServices;
        }

        #region CRUD DB
        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save(SaveGraphDTO taskGraph)
        {
            await _graphServices.SaveGraph(taskGraph);    
            return Ok();
        }

        [HttpGet]
        [Route("{graphId}")]
        public async Task<ActionResult> GetGraph(string graphId)
        {
            var validGuid = Guid.TryParse(graphId, out Guid parsedGuid);
            if (!validGuid)
            {
                return BadRequest();
            }
            var result = await _graphServices.GetGraphWithGuid(parsedGuid);
            return new JsonResult(new
            {
               graph = result,
            });
        }
        #endregion

        #region Generating
        #region GET
        /// <summary>
        /// Generates a standard task graph. Does not include any scheduling or critical path
        /// </summary>
        /// <returns>
        /// A standard task graph
        /// </returns>
        [HttpGet]
        [Route("generate/standard")]
        public IActionResult Standard()
        {
            var graph = _dagServices.CreateDag();
            return new JsonResult(new
            {
                graph,
            });
        }

        #endregion
        #region POST
        [HttpPost]
        [Route("generate/standard")]
        public IActionResult Standard(GenerateStandardGraphDTO graphDetails)
        {
            return new JsonResult(new
            {
                graph = _dagServices.CreateDag(graphDetails),
            });
        }

        [HttpPost]
        [Route("generate/critical-path")]
        [Authorize]
        public IActionResult CriticalPath(GenerateCriticalGraphDTO graphDetails)
        {
            var graph = _dagServices.CreateCriticalPathDag(graphDetails);
            var scheduled = _dagScheduleServices.ScheduleGraph(graphDetails.Processors, graph);
            return new JsonResult(new
            {
                graph,
                schedule = scheduled
            });
        }
        #endregion
        #endregion

        #region Scheduling
        [HttpPost]
        [Route("schedule/reschedule")]
        [Authorize]
        public IActionResult ReSchedule(RescheduleGraphDTO scheduledGraph)
        {
            var schedule = _dagScheduleServices.ScheduleGraph(scheduledGraph.Processors, scheduledGraph.TaskGraph);
            return new JsonResult(new
            {
                schedule
            });
        }
        #endregion
    }
}
