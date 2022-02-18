using Core.Server.Common.Logging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Logging
{
    public class LoggingCaller
    {
        [Dependency]
        public IMethodLogger MethodLogger;

        public async Task LogginCall(string entityName, Func<Task> action, LoggingTierLevel loggingTierLevel, object request = null)
        {
            var callName = entityName + "." + GetMethodName(action);
            MethodLogger.MethodStart(loggingTierLevel, callName, request);
            await action();
            MethodLogger.MethodEnded(loggingTierLevel, callName, null);
        }

        public async Task<T> LogginCall<T>(string entityName, Func<Task<T>> action, LoggingTierLevel loggingTierLevel, object request = null)
        {
            var callName = entityName + "." + GetMethodName(action);
            MethodLogger.MethodStart(loggingTierLevel, callName, request);
            var response = await action();
            MethodLogger.MethodEnded(loggingTierLevel, callName, response);
            return response;
        }

        public async Task<ActionResult<T>> LogginCall<T>(Func<Task<ActionResult<T>>> action, object request = null)
        {
            var methodName = GetMethodName(action);
            MethodLogger.MethodStart(LoggingTierLevel.Application, methodName, request);
            var actionResponse = await action();
            object response = actionResponse.Value;
            if (response == null)
                response = actionResponse.Result;
            MethodLogger.MethodEnded(LoggingTierLevel.Application, methodName, response);
            return actionResponse;
        }

        private string GetMethodName<T>(Func<Task<T>> action)
        {
            return GetMethodName(action.Method);
        }

        private string GetMethodName(Func<Task> action)
        {
            return GetMethodName(action.Method);
        }

        private string GetMethodName(MethodInfo methodInfo)
        {
            var methodName = methodInfo.Name;
            return methodName.Substring(1, methodName.LastIndexOf('>') - 1);
        }
    }
}