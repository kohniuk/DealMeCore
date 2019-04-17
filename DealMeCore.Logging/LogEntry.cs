using System;

namespace DealMeCore.Logging
{
    /// <summary>
    /// Log entry.
    /// </summary>
    public class LogEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LogEntry"/> class.
        /// </summary>
        /// <param name="severity">The severity.</param>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <exception cref="System.ArgumentException">
        /// severity
        /// or
        /// message
        /// </exception>
        public LogEntry(LogLevel severity, string message, Exception exception = null)
        {
            if (!Enum.IsDefined(typeof(LogLevel), severity))
            {
                throw new ArgumentException(nameof(severity));
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException(nameof(message));
            }

            this.Severity = severity;
            this.Message = message;
            this.Exception = exception;
        }

        /// <summary>
        /// Severity.
        /// </summary>
        public LogLevel Severity { get; private set; }

        /// <summary>
        /// Message.
        /// </summary>
        public string Message { get; protected set; }

        /// <summary>
        /// Exception.
        /// </summary>
        public Exception Exception { get; private set; }
    }
}
