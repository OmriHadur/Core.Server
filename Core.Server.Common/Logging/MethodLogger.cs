using Core.Server.Common.Attributes;
using Core.Server.Common.Config;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Unity;

namespace Core.Server.Common.Logging
{
    [Inject]
    public class MethodLogger : IMethodLogger
    {
        private readonly Dictionary<string, Stopwatch> stopwatchs;

        [Dependency]
        public LoggingConfig LogginConfig;

        [Dependency]
        public ILogger<MethodLogger> Logger;

        public MethodLogger()
        {
            stopwatchs = new Dictionary<string, Stopwatch>();
        }

        public void MethodStart(LoggingTierLevel loggingTierLevel, string methodName, object request)
        {
            LogInformation(loggingTierLevel, (sb, la) =>
            {
                if (la.HasFlag(LoggingActions.Started))
                    sb.Append($"{loggingTierLevel}.{methodName}:Started ");

                if (la.HasFlag(LoggingActions.Request) & request != null)
                    sb.Append("Request: " + JsonSerializer.Serialize(request));

                if (la.HasFlag(LoggingActions.Took))
                {
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    var key = loggingTierLevel + methodName;
                    if (stopwatchs.ContainsKey(key))
                        stopwatchs.Remove(key);
                    stopwatchs.Add(key, stopwatch);
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
                     var stopwatch = stopwatchs[loggingTierLevel + methodName];
                     stopwatchs.Remove(loggingTierLevel + methodName);
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
