using Dagable.Api.Core;
using Dagable.Core;

namespace Dagable.Api.Services
{
    public interface IDagServices
    {
        string CreateDag();
        string CreateDag(GenerateStandardGraphDTO graphDetails);
        string CreateCriticalPathDag(GenerateCriticalGraphDTO graphsDetails);
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
            return _dagCreationServices.GenerateGraphAsString(GraphType.Standard);
        }

        public string CreateDag(GenerateStandardGraphDTO graphDetails)
        {
            return _dagCreationServices.GenerateGraphAsString(GraphType.Standard, graphDetails.Layers, graphDetails.Nodes, (graphDetails.Percentage / 100));
        }

        public string CreateCriticalPathDag(GenerateCriticalGraphDTO graphDetails)
        {
            return _dagCreationServices.GenerateGraphAsString(GraphType.CriticalPath, graphDetails.MinComp, graphDetails.MaxComp, graphDetails.MinComm, graphDetails.MaxComm, graphDetails.Layers, graphDetails.Nodes, graphDetails.Percentage / 100);
        }
    }
}
