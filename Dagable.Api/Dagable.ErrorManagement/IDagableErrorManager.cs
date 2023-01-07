namespace Dagable.ErrorManagement
{
    public interface IDagableErrorManager
    {
        DagableError this[string code] { get; }
    }
}