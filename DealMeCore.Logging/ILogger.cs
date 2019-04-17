namespace DealMeCore.Logging
{
    /// <summary>
    /// Common interface for logging.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs the specified information.
        /// </summary>
        /// <param name="entry">Log entry.</param>
        void Log(LogEntry entry);
    }
}