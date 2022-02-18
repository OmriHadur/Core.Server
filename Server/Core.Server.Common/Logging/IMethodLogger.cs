namespace Core.Server.Common.Logging
{
    public interface IMethodLogger
    {
        void MethodStart(LoggingTierLevel loggingLevel, string methodName, object request);
        void MethodEnded(LoggingTierLevel loggingLevel, string methodName, object response);
    }
}