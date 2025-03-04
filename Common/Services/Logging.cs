using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Logging
{
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
}
