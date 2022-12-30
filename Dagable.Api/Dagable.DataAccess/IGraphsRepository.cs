using Dagable.Api.Core.Graph;
using Dagable.Core;
using Dagable.DataAccess.Migrations.DbModels;

namespace Dagable.DataAccess
{
    public interface IGraphsRepository
    {
        Task<bool> SaveGraph(Guid userId, SaveGraphDTO taskGraph);
        Task<List<Graph>> FindAllGraphsForUser(Guid userId);
        Task<Graph> FindGraphForGuid(Guid guid, Guid userId);
    }
}