using Dagable.Api.Core;
using Dagable.Core;

namespace Dagable.Api.Services
{
    public interface IDagGenerationServices
    {
        /// <summary>
        /// Method used to create a standard task graph using the defeualt settings
        /// </summary>
        /// TODO: Change this to an actual object.
        /// <returns>string version of the graoh</returns>
        string CreateDag();

        /// <summary>
        /// Method used to create a standard task graph based on settings passed in
        /// </summary>
        /// <param name="graphDetails">The settings object defining the generation settings</param>
        /// TODO: Change this to an actual object.
        /// <returns>string version of the graoh</returns>
        string CreateDag(GenerateStandardGraphDTO graphDetails);

        /// <summary>
        /// Method used to create a critical path task graph
        /// </summary>
        /// <param name="graphsDetails">The settings object</param>
        /// <returns>A critical path  object based on the settings passed in</returns>
        ICriticalPathTaskGraph CreateCriticalPathDag(GenerateCriticalGraphDTO graphsDetails);
    }

    public class DagGenerationServices : IDagGenerationServices
    {
        private readonly IDagCreationService _dagCreationServices;

        public DagGenerationServices(IDagCreationService dagableServices)
        {
            _dagCreationServices = dagableServices;
        }

        /// <inheritdoc cref="IDagGenerationServices.CreateDag"/>
        public string CreateDag()
        {
            return _dagCreationServices.GenerateStandardTaskGraph().ToString();
        }

        /// <inheritdoc cref="IDagGenerationServices.CreateDag(GenerateStandardGraphDTO)"/>
        public string CreateDag(GenerateStandardGraphDTO graphDetails)
        {
            return _dagCreationServices.GenerateStandardTaskGraph(graphDetails.Layers, graphDetails.Nodes, (graphDetails.Percentage / 100)).ToString();
        }

        /// <inheritdoc cref="IDagGenerationServices.CreateCriticalPathDag(GenerateCriticalGraphDTO)"/>
        public ICriticalPathTaskGraph CreateCriticalPathDag(GenerateCriticalGraphDTO graphDetails)
        {
            return _dagCreationServices.GenerateCriticalPathTaskGraph(graphDetails.Layers, graphDetails.Nodes, graphDetails.Percentage / 100, graphDetails.MinComp, graphDetails.MaxComp, graphDetails.MinComm, graphDetails.MaxComm);
        }
    }
}
