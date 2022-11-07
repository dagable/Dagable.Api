using Dagable.Api.Core;
using Dagable.Core;

namespace Dagable.Api.Services
{
    public interface IDagServices
    {
        string CreateDag();
        string CreateDag(GenerateStandardGraphDTO graphDetails);
        ICriticalPathTaskGraph CreateCriticalPathDag(GenerateCriticalGraphDTO graphsDetails);
    }

    public class DagServices : IDagServices
    {
        private readonly IDagCreationService _dagCreationServices;


        public DagServices(IDagCreationService dagableServices)
        {
            _dagCreationServices = dagableServices;
        }

        public string CreateDag()
        {
            return _dagCreationServices.GenerateStandardTaskGraph().ToString();
        }

        public string CreateDag(GenerateStandardGraphDTO graphDetails)
        {
            return _dagCreationServices.GenerateStandardTaskGraph(graphDetails.Layers, graphDetails.Nodes, (graphDetails.Percentage / 100)).ToString();
        }

        public ICriticalPathTaskGraph CreateCriticalPathDag(GenerateCriticalGraphDTO graphDetails)
        {
            return _dagCreationServices.GenerateCriticalPathTaskGraph(graphDetails.Layers, graphDetails.Nodes, graphDetails.Percentage / 100, graphDetails.MinComp, graphDetails.MaxComp, graphDetails.MinComm, graphDetails.MaxComm);
        }
    }
}
