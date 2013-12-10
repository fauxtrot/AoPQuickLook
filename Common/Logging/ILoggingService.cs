namespace Common.Logging
{
    public interface ILoggingService
    {
        void Log(string message);
        void Log(string message, params object[] formatObjects);
    }
}
