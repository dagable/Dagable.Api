using Dagable.Core;
using Dagable.Core.Scheduling;
using System.Collections.Generic;

namespace Dagable.Api.Services
{
    public interface IDagScheduleServices
    {
        Dictionary<int, List<ScheduledNode>> CreateSchedule(int processors);
    }

    public class DagScheduleServices : IDagScheduleServices
    {
        private readonly IDagCreationService _dagCreationServices;
        private readonly ITaskGraphSchedulingService _taskGraphSchedulingService;

        public DagScheduleServices(IDagCreationService dagableServices, ITaskGraphSchedulingService taskGraphSchedulingService)
        {
            _dagCreationServices = dagableServices;
            _taskGraphSchedulingService = taskGraphSchedulingService;
        }

        public Dictionary<int, List<ScheduledNode>> CreateSchedule(int processors)
        {
            var graph = _dagCreationServices.GenerateCriticalPathTaskGraph();
            return _taskGraphSchedulingService.DLSchedule(processors, graph);
        }
    }
}
