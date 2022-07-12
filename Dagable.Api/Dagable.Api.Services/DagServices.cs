using Dagable.Api.RequestModels;
using Dagable.Core;

namespace Dagable.Api.Services
{
    public interface IDagServices
    {
        string CreateDag();
        string CreateDag(GenerateGraphDTO graphDetails);
        string CreateCriticalPathDag();
    }

    public class DagServices : IDagServices
    {
        public string CreateDag()
        {
            var dagGraph = new DagCreator.Standard().Generate();
            return dagGraph.AsJson();
        }

        public string CreateDag(GenerateGraphDTO graphDetails)
        {
            var dagGraph = new DagCreator.Standard(graphDetails.Layers, graphDetails.Nodes, (graphDetails.Percentage / 100)).Generate();
            return dagGraph.AsJson();
        }

        public string CreateCriticalPathDag()
        {
            var cpDagGraph = new DagCreator.CriticalPath().Generate();
            return cpDagGraph.AsJson();
        }
    }
}
