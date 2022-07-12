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
        public string CreateDag()
        {
            var dagGraph = new DagCreator.Standard().Generate();
            return dagGraph.AsJson();
        }

        public string CreateDag(GenerateStandardGraphDTO graphDetails)
        {
            var dagGraph = new DagCreator.Standard(graphDetails.Layers, graphDetails.Nodes, (graphDetails.Percentage / 100)).Generate();
            return dagGraph.AsJson();
        }

        public string CreateCriticalPathDag(GenerateCriticalGraphDTO graphDetails)
        {
            var cpDagGraph = new DagCreator.CriticalPath(graphDetails.MinComp, graphDetails.MaxComp, graphDetails.MinComm, graphDetails.MaxComm, graphDetails.Layers, graphDetails.Nodes, graphDetails.Percentage / 100).Generate();
            return cpDagGraph.AsJson();
        }
    }
}
