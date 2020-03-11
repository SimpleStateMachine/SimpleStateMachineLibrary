using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleStateMachineLibrary.Helpers
{
    static internal class LogExtension
    {
        static internal void LogDebugAndInformation(this ILogger logger, string message, params object[] args)
        {
            logger?.LogDebug(message, args);
            logger?.LogInformation(message, args);
        }
    }
}
