using Utils.Singletons;

namespace Client.Utils.Logger
{
    public class FileLogger : ILogger, IDisposable
    {
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default!;

        private readonly Func<FileLoggerConfiguration> _getCurrentConfig;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLogger"/> class.
        /// </summary>
        /// <param name="getCurrentConfig">A function that returns the current configuration for the logger.</param>
        public FileLogger(Func<FileLoggerConfiguration> getCurrentConfig) => (_getCurrentConfig) = (getCurrentConfig);

        /// <summary>
        /// Determines whether logging is enabled for the specified log level.
        /// </summary>
        /// <param name="logLevel">The log level to check.</param>
        /// <returns>true if logging is enabled for the specified log level; otherwise, false.</returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return _getCurrentConfig().LogLevels.ContainsKey(logLevel);
        }

        /// <summary>
        /// Writes a log message to the file.
        /// </summary>
        /// <typeparam name="TState">The type of the state object.</typeparam>
        /// <param name="logLevel">The log level of the message.</param>
        /// <param name="eventId">The ID of the event.</param>
        /// <param name="state">The state object.</param>
        /// <param name="exception">The exception associated with the message, if any.</param>
        /// <param name="formatter">A function that formats the message.</param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            // If an exception was passed in, write its full stack trace to the log file.
            if (exception != null)
                WriteFullStack(logLevel, exception);
        }

        private void WriteFullStack(LogLevel logLevel, Exception? exception)
        {
            if (!IsEnabled(logLevel))
                return;

            if (exception == null) return;

            // Open a stream to the log file and write the exception message and stack trace.
            StreamWriter fileStream = File.AppendText(Path.Combine(EnvironementSingleton.WebRootPath, "CustomLog.log"));
            fileStream.WriteLine($"{logLevel} : {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()} : {exception?.Message}");
            fileStream.WriteLine($"{exception?.StackTrace}");
            fileStream.Close();
            Console.WriteLine(exception);
            
            // If the exception has an inner exception, write its full stack trace as well.
            if (exception.InnerException != null)
                WriteFullStack(logLevel, exception.InnerException);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}