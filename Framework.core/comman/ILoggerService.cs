

using Framework.comman.Enums;

namespace Framework.core.comman
{
    public interface ILoggerService
    {

        void log(string message,LogType logType);

        void logInf(string message);
        void logError(string message);
        void logError(Exception exception);
        void logWarning(string message);
        void logText(string message);
    }
}
