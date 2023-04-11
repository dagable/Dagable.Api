using Dagable.Core;
using Dagable.Core.Scheduling;
using Dagable.Core.Scheduling.Models.DTO;

namespace Dagable.Api.Services
{
    public interface IDagScheduleServices
    {
        /// <summary>
        /// Method used to schedule a critical path task graph
        /// </summary>
        /// <param name="processors">The number of processors that can be used when scheduling</param>
        /// <param name="graph">The graph we want to schedule</param>
        /// <returns>A scheduled task graph</returns>
        IScheduledGraph ScheduleGraph(int processors, ICriticalPathTaskGraph graph = null);
    }

    public class GraphSchedulingServices : IDagScheduleServices
    {
        private readonly IDagCreationService _dagCreationServices;
        private readonly ITaskGraphSchedulingService _taskGraphSchedulingService;

        public GraphSchedulingServices(IDagCreationService dagableServices, ITaskGraphSchedulingService taskGraphSchedulingService)
        {
            _dagCreationServices = dagableServices;
            _taskGraphSchedulingService = taskGraphSchedulingService;
        }

        /// <inheritdoc cref="IDagScheduleServices.ScheduleGraph(int, ICriticalPathTaskGraph)"/>
        public IScheduledGraph ScheduleGraph(int processors, ICriticalPathTaskGraph graph = null)
        {
            graph ??= _dagCreationServices.GenerateCriticalPathTaskGraph();
            return _taskGraphSchedulingService.DLSchedule(processors, graph);
        }
    }
}
