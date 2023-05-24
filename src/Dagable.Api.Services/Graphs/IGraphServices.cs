using Dagable.Api.Core.Graph;
using Dagable.Core;
using System;
using System.Threading.Tasks;

namespace Dagable.Api.Services.Graphs
{
    public interface IGraphServices
    {
        /// <summary>
        /// Method used to save a graph to the databse against the user
        /// </summary>
        /// <param name="saveGraph">The graph that we want to save to the database</param>
        /// <returns>a boolean result if the save was successful</returns>
        Task<bool> SaveGraph(SaveGraphDTO saveGraph);

        /// <summary>
        /// Method used to get a graph with a specific guid
        /// </summary>
        /// <param name="guid">the graph guid that we want to find</param>
        /// <returns>The graph object for the given guid</returns>
        Task<ICriticalPathTaskGraph> GetGraphWithGuid(Guid guid);
    }
}