using Dagable.Api.Core.Exceptions;
using Dagable.Api.Core.Graph;
using Dagable.Core;
using Dagable.Core.Models;
using Dagable.DataAccess;
using Dagable.ErrorManagement;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dagable.Api.Services.Graphs
{
    public class GraphServices : IGraphServices
    {
        private readonly IGraphsRepository _graphsRepository;
        private readonly IUserServices _userServices;
        private readonly IDagableErrorManager _dagableErrorManager;

        public GraphServices(IGraphsRepository graphsRepository, IUserServices userServices, IDagableErrorManager dagableErrorManager)
        {
            _graphsRepository = graphsRepository;
            _userServices = userServices;
            _dagableErrorManager = dagableErrorManager;
        }   

        /// <inheritdoc cref="IGraphServices.SaveGraph(SaveGraphDTO)" />
        public async Task<bool> SaveGraph(SaveGraphDTO saveGraph)
        {
            return await _graphsRepository.SaveGraph(_userServices.GetLoggedInUserId(), saveGraph);
        }

        ///<inheritdoc cref="IGraphServices.GetGraphWithGuid(Guid)" />
        public async Task<ICriticalPathTaskGraph> GetGraphWithGuid(Guid guid)
        {
            var userId = _userServices.GetLoggedInUserId();
            var result = await _graphsRepository.FindGraphForGuid(guid, userId);
            if (result == null)
            {
                _dagableErrorManager.LogWarningAndThrowException<NotFoundException>($"Graph with guid {guid} not found for user {userId}.");
            }

            var graph = new TaskGraph.CriticalPath(result.Nodes.Max(x => x.Layer))
            {
                dagGraph = new Graph<CriticalPathNode, CriticalPathEdge>()
            };

            var nodes = result.Nodes.Select(x => new CriticalPathNode(x.GraphNodeId, x.Layer, (int)x.CompTime)).ToList();

            nodes.ForEach(x => graph.dagGraph.AddNode(x));
            result.Edges.ToList().ForEach(
                x => graph.dagGraph.AddEdge(new CriticalPathEdge(
                    nodes.First(n => n.Id == x.From.GraphNodeId),
                    nodes.First(n => n.Id == x.To.GraphNodeId),
                    (int)x.CommTime)
                )
            );

            graph.DetermineCriticalPathLength();

            return graph;
        }
    }
}
