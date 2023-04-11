using Dagable.Api.Core.Graph;
using Dagable.Core;
using Dagable.DataAccess.Migrations.DbModels;

namespace Dagable.DataAccess
{
    public interface IGraphsRepository
    {
        /// <summary>
        /// Method used to save a graph to the database
        /// </summary>
        /// <param name="userId">The user id to save the graph against</param>
        /// <param name="taskGraph">The task graph that we want to save</param>
        /// <returns>true if success, false otherwise</returns>
        Task<bool> SaveGraph(Guid userId, SaveGraphDTO taskGraph);
        /// <summary>
        /// Finds and returns all of the graphs for a user
        /// </summary>
        /// <param name="userId">The userID we want to find all the graphs for</param>
        /// <returns>A list of graphs against the user</returns>
        Task<List<Graph>> FindAllGraphsForUser(Guid userId);
        /// <summary>
        /// Find a specific graph given a graph Guid
        /// </summary>
        /// <param name="guid">the graph guid</param>
        /// <param name="userId">The user Id for security checks</param>
        /// <returns>The graph if found</returns>
        Task<Graph> FindGraphForGuid(Guid guid, Guid userId);
    }
}