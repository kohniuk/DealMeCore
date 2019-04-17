using NLog;

namespace DealMeCore.Logging.NLog
{
    /// <summary>
    /// Adapter for NLog logging library.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NLogAdapter<T> : ILogger<T>
    {

        private readonly global::NLog.ILogger logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="NLogAdapter{T}" /> class.
        /// </summary>
        public NLogAdapter()
        {
            this.logger = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// Logs the specified information.
        /// </summary>
        /// <param name="entry"></param>
        public void Log(LogEntry entry)
        {
            switch (entry.Severity)
            {
                case LogLevel.Information:
                    {
                        logger.Info(entry.Exception, entry.Message);
                        break;
                    }

                case LogLevel.Debug:
                    {
                        logger.Debug(entry.Exception, entry.Message);

                        break;
                    }

                case LogLevel.Warning:
                    {
                        logger.Warn(entry.Exception, entry.Message);

                        break;
                    }

                case LogLevel.Error:
                    {
                        logger.Error(entry.Exception, entry.Message);

                        break;
                    }

                default:
                    {
                        logger.Fatal(entry.Exception, entry.Message);

                        break;
                    }
            }
        }
    }
}
