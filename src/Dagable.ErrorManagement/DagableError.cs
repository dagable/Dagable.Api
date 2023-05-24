namespace Dagable.ErrorManagement
{
    public class DagableError
    {
        public string Code { get; }
        public string Description { get; }
        public DagableError(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}
