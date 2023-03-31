using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace Client.Utils.Logger
{
    [ProviderAlias("FileLogger")]
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly IDisposable _onChangeToken;
        private FileLoggerConfiguration _currentConfig;

        private readonly ConcurrentDictionary<string, FileLogger> _loggers = new(StringComparer.OrdinalIgnoreCase);

        public FileLoggerProvider(IOptionsMonitor<FileLoggerConfiguration> config)
        {
            _currentConfig = config.CurrentValue;
            _onChangeToken = config.OnChange(updatedConfig => _currentConfig = updatedConfig);
        }

        public ILogger CreateLogger(string categoryName) => _loggers.GetOrAdd(categoryName, name => new FileLogger(GetCurrentConfig));
        private FileLoggerConfiguration GetCurrentConfig() => _currentConfig;


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            _loggers.Clear();
            _onChangeToken.Dispose();
        }
    }
}