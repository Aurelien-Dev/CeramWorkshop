using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;

namespace Client.Utils.Logger
{
    public static class FileLoggerExtensions
    {
        public static ILoggingBuilder AddMyCustomLogger(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, FileLoggerProvider>());

            LoggerProviderOptions.RegisterProviderOptions<FileLoggerConfiguration, FileLoggerProvider>(builder.Services);

            return builder;
        }

        public static ILoggingBuilder AddMyCustomLogger(this ILoggingBuilder builder, Action<FileLoggerConfiguration> configure)
        {
            builder.AddMyCustomLogger();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
