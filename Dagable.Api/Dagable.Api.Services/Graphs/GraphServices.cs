using Dagable.Api.Core.Graph;
using Dagable.Core;
using Dagable.Core.Models;
using Dagable.DataAccess;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Dagable.Api.Services.Graphs
{
    public class GraphServices : IGraphServices
    {
        private readonly IGraphsRepository _graphsRepository;
        private readonly IUserServices _userServices;
        private readonly ILogger<GraphServices> _logger;

        public GraphServices(ILogger<GraphServices> logger, IGraphsRepository graphsRepository, IUserServices userServices)
        {
            _logger = logger;
            _graphsRepository = graphsRepository;
            _userServices = userServices;
        }

        public async Task<bool> SaveGraph(SaveGraphDTO saveGraph)
        {
            return await _graphsRepository.SaveGraph(_userServices.GetLoggedInUserId(), saveGraph);
        }

        public async Task<ICriticalPathTaskGraph> GetGraphWithGuid(Guid guid)
        {
            var result = await _graphsRepository.FindGraphForGuid(guid, _userServices.GetLoggedInUserId());
            if (result == null)
            {
                _logger.LogError($"Graph with guid {guid} not found.");
                throw new Exception($"Graph with guid {guid} not found.");
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
                    (int) x.CommTime)
                )
            );

            var s = graph.dagGraph.Edges;

            graph.DetermineCriticalPathLength();

            return graph;
        }
    }
}
