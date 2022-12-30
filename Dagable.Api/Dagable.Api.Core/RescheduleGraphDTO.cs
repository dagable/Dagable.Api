using Dagable.Core;

namespace Dagable.Api.Core
{
    public class RescheduleGraphDTO
    {
        public int Processors { get;  set; }
        public ICriticalPathTaskGraph TaskGraph { get; set; }
    }
}
