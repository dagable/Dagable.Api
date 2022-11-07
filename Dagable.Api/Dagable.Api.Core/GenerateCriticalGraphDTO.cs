namespace Dagable.Api.Core
{
    public class GenerateCriticalGraphDTO : GenerateStandardGraphDTO
    {
        public int MaxComm { get; set; }
        public int MinComm { get; set; }
        public int MaxComp { get; set;}
        public int MinComp { get; set; }
        public int Processors { get; set; }
    }
}
