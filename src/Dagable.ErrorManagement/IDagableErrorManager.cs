namespace Dagable.ErrorManagement
{
    public interface IDagableErrorManager
    {
        DagableError this[string code] { get; }

        void LogErrorAndThrowException<T>(string message) where T : Exception;

        void LogWarningAndThrowException<T>(string message) where T : Exception;

    }
}