using Common.Utils.Singletons;

namespace Client.Utils.Logger
{
    public class FileLogger : ILogger, IDisposable
    {
        public IDisposable BeginScope<TState>(TState state) => default!;
        private readonly Func<FileLoggerConfiguration> _getCurrentConfig;

        public FileLogger(Func<FileLoggerConfiguration> getCurrentConfig) => (_getCurrentConfig) = (getCurrentConfig);

        public bool IsEnabled(LogLevel logLevel)
        {
            return _getCurrentConfig().LogLevels.ContainsKey(logLevel);
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {

            if (exception != null)
                WriteFullStack(logLevel, exception);

        }

        private void WriteFullStack(LogLevel logLevel, Exception exception)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            StreamWriter fileStream = File.AppendText(Path.Combine(EnvironementSingleton.WebRootPath, "CustomLog.log"));
            fileStream.WriteLine($"{logLevel} : {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()} : {exception?.Message}");
            fileStream.WriteLine($"{exception?.StackTrace}");
            fileStream.Close();

            if (exception.InnerException != null)
                WriteFullStack(logLevel, exception.InnerException);
        }




        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
