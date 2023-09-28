using Framework.comman.Enums;
using Framework.core.comman;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Serilog;
using Serilog.Settings.Configuration;

namespace Service.Business.Services.Comman
{

    public class LoggerService : ILoggerService
    {
        private readonly ILogger _logger;
        private IConfiguration _configuration;

        public LoggerService(IConfiguration configuration)
        {
            _configuration = configuration;

            _logger = new LoggerConfiguration()
                          .ReadFrom.Configuration(_configuration, new ConfigurationReaderOptions { SectionName = "Serilog" })
                          .MinimumLevel.Information()
                          .Enrich.FromLogContext()
                          .CreateLogger();
        }

        public void log(string message, LogType logType)
        {
            try
            {
                switch(logType)
                {
                    case LogType.Error:
                        _logger.Error(message);
                        break;
                    case LogType.Warning:
                        _logger.Warning(message);
                        break;
                    case LogType.Information:
                        _logger.Information(message);
                        break;
                    case LogType.Text:
                        _logger.Debug(message);
                        break;
                    default:
                        break;

                }
            }
            catch (Exception ex)
            {

                try
                {
                    EventLog eventLog = new EventLog(this.GetType().FullName, System.Environment.MachineName);

                    eventLog.WriteEntry(ex.ToString(), EventLogEntryType.Error);
                }
                catch { }

            }
        }

        public void logError(string message)
        {
            log(message,LogType.Error);
        }

        public void logError(Exception exception)
        {
            log(exception.ToString(),LogType.Error);
        }

        public void logInf(string message)
        {
            log(message, LogType.Information);
        }

        public void logText(string message)
        {
            log(message, LogType.Text);
        }

        public void logWarning(string message)
        {
            log(message, LogType.Warning);
        }

    }


}

