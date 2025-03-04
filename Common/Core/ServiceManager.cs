using Microsoft.Extensions.Logging;

namespace GatewayAPI
{
    public class ServiceManager
    {
        #region Constructor
        public ServiceManager(IServiceProvider serviceProvider, ILogger<ServiceManager> logger)
        { }
        #endregion

        #region Methods
        public static class LogHelper
        {
            public static void LogInformation<T>(ILogger<T> logger, string message, params object[] args)
            {
                logger.LogInformation(message, args);
            }

            public static void LogWarning<T>(ILogger<T> logger, string message, params object[] args)
            {
                logger.LogWarning(message, args);
            }

            public static void LogError<T>(ILogger<T> logger, Exception ex, string message, params object[] args)
            {
                logger.LogError(ex, message, args);
            }
        }

        #endregion
    }
}
