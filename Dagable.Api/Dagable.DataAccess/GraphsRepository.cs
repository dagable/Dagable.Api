using Dagable.Core;
using Dagable.DataAccess.Migrations.DbModels;
using Microsoft.Extensions.Logging;

namespace Dagable.DataAccess
{
    public class GraphsRepository
    {
        private readonly ILogger<GraphsRepository> _logger;

        public GraphsRepository(ILogger<GraphsRepository> logger) {
            _logger = logger;
        }

        public async Task<bool> SaveGraph(ICriticalPathTaskGraph taskGraph)
        {
            /*var graph = new Graph()
            {
                CreatedAt = DateTime.Now,
                Name= taskGraph.,
            }*/


            return true;
        }
    }
}