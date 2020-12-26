using Core.Server.Common.Attributes;
using Core.Server.Common.Config;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
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
                if (la >= LoggingActions.Method)
                    sb.Append($"{loggingTierLevel}.{methodName}:Started ");

                if (la >= LoggingActions.MethodsTimeInputOutput)
                    sb.Append("Request: " + JsonConvert.SerializeObject(request));

                if (la >= LoggingActions.MethodsTime)
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
                 if (la == LoggingActions.Method)
                     sb.Append($"{loggingTierLevel}.{methodName}:Finished ");

                 if (la >= LoggingActions.MethodsTime)
                 {
                     var stopwatch = stopwatchs[loggingTierLevel + methodName];
                     stopwatchs.Remove(loggingTierLevel + methodName);
                     stopwatch.Stop();
                     sb.Append($"{loggingTierLevel}.{methodName} took: {stopwatch.ElapsedMilliseconds} Milliseconds ");
                 }

                 if (la == LoggingActions.MethodsTimeInputOutput)
                     sb.Append($"Response: {JsonConvert.SerializeObject(response)}");
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
