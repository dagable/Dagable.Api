using Dagable.Core;
using Dagable.Core.Scheduling;
using Dagable.Core.Scheduling.Models.DTO;
using System.Collections.Generic;

namespace Dagable.Api.Services
{
    public interface IDagScheduleServices
    {
        IScheduledGraph CreateSchedule(int processors, ICriticalPathTaskGraph graph = null);
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

        public IScheduledGraph CreateSchedule(int processors, ICriticalPathTaskGraph graph = null)
        {
            if(graph == null)
            {
                graph = _dagCreationServices.GenerateCriticalPathTaskGraph();
            }
            return _taskGraphSchedulingService.DLSchedule(processors, graph);
        }
    }
}
