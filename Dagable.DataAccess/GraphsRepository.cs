using Dagable.Api.Core.Graph;
using Dagable.DataAccess.Migrations;
using Dagable.DataAccess.Migrations.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Dagable.DataAccess
{
    public class GraphsRepository : IGraphsRepository
    {
        private readonly ILogger<GraphsRepository> _logger;
        private readonly DagableDbContext _dbContext;

        public GraphsRepository(ILogger<GraphsRepository> logger, DagableDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        /// <inheritdoc cref="IGraphsRepository.FindGraphForGuid(Guid, Guid)" />
        public async Task<Graph> FindGraphForGuid(Guid guid, Guid userId)
        {
            var result = await _dbContext.Graphs
                    .Include(x => x.Edges)
                    .Include(x => x.Nodes)
                    .FirstOrDefaultAsync(x => x.GraphGuid == guid && x.UserId == userId);

            return result;
        }

        /// <inheritdoc cref="IGraphsRepository.FindAllGraphsForUser(Guid)" />
        public async Task<List<Graph>> FindAllGraphsForUser(Guid userId)
        {
            return await _dbContext.Graphs.Where(x => x.UserId == userId).ToListAsync();
        }

        /// <inheritdoc cref="IGraphsRepository.FindAllGraphsForUser(Guid, SaveGraphDTO)" />
        public async Task<bool> SaveGraph(Guid userId, SaveGraphDTO taskGraph)
        {
            var graph = new Graph()
            {
                Name = taskGraph.Name,
                Description = taskGraph.Description,
                UserId = userId,
                GraphGuid = Guid.NewGuid()
            };

            HashSet<Edge> edges = new();
            HashSet<Node> nodes = new();

            foreach (var edge in taskGraph.TaskGraph.GetCriticalPathEdges)
            {
                nodes.Add(new Node
                {
                    CompTime = edge.PrevNode.ComputationTime,
                    Graph = graph,
                    IsCritical = true,
                    Layer = edge.PrevNode.Layer,
                    Label = edge.PrevNode.Id.ToString(),
                    GraphNodeId = edge.PrevNode.Id
                });

                nodes.Add(new Node
                {
                    CompTime = edge.NextNode.ComputationTime,
                    Graph = graph,
                    Layer = edge.NextNode.Layer,
                    IsCritical = true,
                    Label = edge.NextNode.Id.ToString(),
                    GraphNodeId = edge.NextNode.Id
                });

                edges.Add(new Edge
                {
                    CommTime = edge.CommTime,
                    Graph = graph,
                    IsCritical = true,
                    From = nodes.First(x => x.GraphNodeId == edge.PrevNode.Id),
                    To = nodes.First(x => x.GraphNodeId == edge.NextNode.Id),
                });
            }

            foreach (var edge in taskGraph.TaskGraph.Edges)
            {
                if (!edges.Any(x => x.To.GraphNodeId == edge.NextNode.Id && x.From.GraphNodeId == edge.PrevNode.Id))
                {
                    if (!nodes.Any(x => x.GraphNodeId == edge.PrevNode.Id))
                    {
                        nodes.Add(new Node
                        {
                            CompTime = edge.PrevNode.ComputationTime,
                            Graph = graph,
                            Layer = edge.PrevNode.Layer,
                            IsCritical = false,
                            Label = edge.PrevNode.Id.ToString(),
                            GraphNodeId = edge.PrevNode.Id
                        });
                    }
                    if (!nodes.Any(x => x.GraphNodeId == edge.NextNode.Id))
                    {
                        nodes.Add(new Node
                        {
                            CompTime = edge.NextNode.ComputationTime,
                            Graph = graph,
                            IsCritical = false,
                            Layer = edge.NextNode.Layer,
                            Label = edge.NextNode.Id.ToString(),
                            GraphNodeId = edge.NextNode.Id
                        });
                    }
                    edges.Add(new Edge
                    {
                        CommTime = edge.CommTime,
                        Graph = graph,
                        IsCritical = false,
                        From = nodes.First(x => x.GraphNodeId == edge.PrevNode.Id),
                        To = nodes.First(x => x.GraphNodeId == edge.NextNode.Id),
                    });
                }
            }

            _dbContext.Edges.AddRange(edges);
            _dbContext.Nodes.AddRange(nodes);
            _dbContext.Graphs.Add(graph);

            return await _dbContext.SaveChangesAsync() > 0;
        }
    }
}