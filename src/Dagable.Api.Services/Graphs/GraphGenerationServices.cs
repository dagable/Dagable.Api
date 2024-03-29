﻿using Dagable.Api.Core.Graph;
using Dagable.Core;

namespace Dagable.Api.Services
{
    public interface IDagGenerationServices
    {
        /// <summary>
        /// Method used to create a standard task graph using the defeualt settings
        /// </summary>
        /// <returns>string version of the graoh</returns>
        IStandardTaskGraph<StandardNode, StandardEdge<StandardNode>> CreateDag();

        /// <summary>
        /// Method used to create a standard task graph based on settings passed in
        /// </summary>
        /// <param name="graphDetails">The settings object defining the generation settings</param>
        /// <returns>string version of the graoh</returns>
        IStandardTaskGraph<StandardNode, StandardEdge<StandardNode>> CreateDag(GenerateStandardGraphDTO graphDetails);

        /// <summary>
        /// Method used to create a critical path task graph
        /// </summary>
        /// <param name="graphsDetails">The settings object</param>
        /// <returns>A critical path  object based on the settings passed in</returns>
        ICriticalPathTaskGraph CreateCriticalPathDag(GenerateCriticalGraphDTO graphsDetails);
    }

    public class GraphGenerationServices : IDagGenerationServices
    {
        private readonly IDagCreationService _dagCreationServices;

        public GraphGenerationServices(IDagCreationService dagableServices)
        {
            _dagCreationServices = dagableServices;
        }

        /// <inheritdoc cref="IDagGenerationServices.CreateDag"/>
        public IStandardTaskGraph<StandardNode, StandardEdge<StandardNode>> CreateDag()
        {
            return _dagCreationServices.GenerateStandardTaskGraph();
        }

        /// <inheritdoc cref="IDagGenerationServices.CreateDag(GenerateStandardGraphDTO)"/>
        public IStandardTaskGraph<StandardNode, StandardEdge<StandardNode>> CreateDag(GenerateStandardGraphDTO graphDetails)
        {
            return _dagCreationServices.GenerateStandardTaskGraph(graphDetails.Layers, graphDetails.Nodes, (graphDetails.Percentage / 100));
        }

        /// <inheritdoc cref="IDagGenerationServices.CreateCriticalPathDag(GenerateCriticalGraphDTO)"/>
        public ICriticalPathTaskGraph CreateCriticalPathDag(GenerateCriticalGraphDTO graphDetails)
        {
            return _dagCreationServices.GenerateCriticalPathTaskGraph(graphDetails.Layers, graphDetails.Nodes, graphDetails.Percentage / 100, graphDetails.MinComp, graphDetails.MaxComp, graphDetails.MinComm, graphDetails.MaxComm);
        }
    }
}
