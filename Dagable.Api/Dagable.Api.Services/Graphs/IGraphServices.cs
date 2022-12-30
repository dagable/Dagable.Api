using Dagable.Api.Core.Graph;
using Dagable.Core;
using System;
using System.Threading.Tasks;

namespace Dagable.Api.Services.Graphs
{
    public interface IGraphServices
    {
        Task<bool> SaveGraph(SaveGraphDTO saveGraph);
        Task<ICriticalPathTaskGraph> GetGraphWithGuid(Guid guid);
    }
}