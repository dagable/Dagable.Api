using Dagable.Api.RequestModels;
using Dagable.Core;

namespace Dagable.Api.Services
{
    public interface IDagServices
    {
        string CreateDag();

        string CreateDag(GenerateGraphDTO grapDetails);
    }

    public class DagServices : IDagServices
    {
        public string CreateDag()
        {
            var dagGraph = new DagCreator().Generate();
            return dagGraph.AsJson();
        }

        public string CreateDag(GenerateGraphDTO grapDetails)
        {
            var dagGraph = new DagCreator(grapDetails.Layers, grapDetails.Nodes, (grapDetails.Percentage / 100)).Generate();
            return dagGraph.AsJson();
        }
    }
}
