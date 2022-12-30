using Dagable.Core;

namespace Dagable.Api.Core.Graph
{
    public class SaveGraphDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICriticalPathTaskGraph TaskGraph { get; set; }
    }
}
