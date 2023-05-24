using Dagable.Api.Core.Graph;
using Dagable.Api.Services;
using Dagable.Api.Services.Graphs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Dagable.Api.Controllers
{
    [Route("[controller]"), ApiController]
    public class GraphsController : BaseController<GraphsController>
    {

        private readonly IDagScheduleServices _dagScheduleServices;
        private readonly IDagGenerationServices _dagServices;
        private readonly IGraphServices _graphServices;

        public GraphsController(ILogger<GraphsController> logger, IDagGenerationServices dagServices, IDagScheduleServices dagScheduleServices, IGraphServices graphServices) : base(logger)
        {
            _dagServices = dagServices;
            _dagScheduleServices = dagScheduleServices;
            _graphServices = graphServices;
        }

        #region CRUD DB
        /// <summary>
        /// Saves a graph to the database for the user.
        /// </summary>
        /// <param name="taskGraph">A task graph that is required for saving.</param>
        /// <returns>Ok when the graph is saved successfully</returns>
        /// <remarks>
        /// </remarks>
        [HttpPost]
        [Route("save")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Save(SaveGraphDTO taskGraph)
        {
            await _graphServices.SaveGraph(taskGraph);
            return Ok();
        }

        [HttpGet]
        [Route("{graphId}")]
        [Authorize]
        public async Task<ActionResult> GetGraph(string graphId)
        {
            var validGuid = Guid.TryParse(graphId, out Guid parsedGuid);
            if (!validGuid)
            {
                return BadRequest();
            }
            var result = await _graphServices.GetGraphWithGuid(parsedGuid);
            return Ok(new { result });
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
            return Ok(new { graph });
        }

        #endregion
        #region POST
        [HttpPost]
        [Route("generate/standard")]
        public IActionResult Standard(GenerateStandardGraphDTO graphDetails)
        {
            var graph = _dagServices.CreateDag(graphDetails);
            return Ok(new { graph });
        }

        [HttpPost]
        [Route("generate/critical-path")]
        [Authorize]
        public IActionResult CriticalPath(GenerateCriticalGraphDTO graphDetails)
        {
            var graph = _dagServices.CreateCriticalPathDag(graphDetails);
            var schedule = _dagScheduleServices.ScheduleGraph(graphDetails.Processors, graph);
            return Ok(new { graph, schedule });
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
            return Ok(schedule);
        }
        #endregion
    }
}
