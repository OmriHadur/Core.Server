using Core.Server.Common.Config;
using Core.Server.Common.Logging;
using Core.Server.Injection.Attributes;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Unity;

namespace Core.Server.Application.Logging
{
    [Inject]
    public class MethodLogger : IMethodLogger
    {
        private readonly Dictionary<string, Stopwatch> Stopwatchs;

        [Dependency]
        public LoggingConfig LogginConfig;

        [Dependency]
        public ILogger<MethodLogger> Logger;

        public MethodLogger()
        {
            Stopwatchs = new Dictionary<string, Stopwatch>();
        }

        public void MethodStart(LoggingTierLevel loggingTierLevel, string methodName, object request)
        {
            LogInformation(loggingTierLevel, (sb, la) =>
            {
                if (la.HasFlag(LoggingActions.Started))
                    sb.Append($"{loggingTierLevel}.{methodName}:Started ");

                if (la.HasFlag(LoggingActions.Request))
                    sb.Append("Request: " + JsonSerializer.Serialize(request));

                if (la.HasFlag(LoggingActions.Took))
                {
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    Stopwatchs.Add(loggingTierLevel + methodName, stopwatch);
                }
            });
        }

        public void MethodEnded(LoggingTierLevel loggingTierLevel, string methodName, object response)
        {
            LogInformation(loggingTierLevel, (sb, la) =>
             {
                 if (la.HasFlag(LoggingActions.Finished) || !la.HasFlag(LoggingActions.Took))
                     sb.Append($"{loggingTierLevel}.{methodName}:Finished ");

                 if (la.HasFlag(LoggingActions.Took))
                 {
                     var stopwatch = Stopwatchs[loggingTierLevel + methodName];
                     Stopwatchs.Remove(loggingTierLevel + methodName);
                     stopwatch.Stop();
                     sb.Append($"{loggingTierLevel}.{methodName} took: {stopwatch.ElapsedMilliseconds} Milliseconds");
                 }

                 if (la.HasFlag(LoggingActions.Response))
                     sb.Append($"Response: {JsonSerializer.Serialize(response)}");
             });
        }

        private void LogInformation(LoggingTierLevel loggingTierLevel, Action<StringBuilder, LoggingActions> action)
        {
            var stringBuilder = new StringBuilder();
            var logginActions = GetLoggingAction(loggingTierLevel);
            action(stringBuilder, logginActions);
            if (stringBuilder.Length != 0)
                Logger.LogInformation(stringBuilder.ToString());
        }

        private LoggingActions GetLoggingAction(LoggingTierLevel loggingTierLevel)
        {
            return loggingTierLevel == LoggingTierLevel.Application ? LogginConfig.Application : LogginConfig.Repository;
        }
    }
}
