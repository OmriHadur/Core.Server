using Core.Server.Common.Applications;
using Core.Server.Common.Logging;
using Core.Server.Shared.Resources.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Logging
{
    public abstract class LoggingApplication<TApplication> 
        : IBaseApplication
        where TApplication: IBaseApplication
    {
        [Dependency]
        public IMethodLogger MethodLogger;

        [Dependency]
        public TApplication Application;

        public UserResource CurrentUser
        {
            get => Application.CurrentUser;
            set { Application.CurrentUser = value; }
        }

        public async Task<ActionResult<T>> CallApplicationWithLog<T>(Func<Task<ActionResult<T>>> action,object request)
        {
            var stackTrace = new StackTrace();
            var methodName = stackTrace.GetFrame(4).GetMethod().Name;
            MethodLogger.MethodStart(LoggingTierLevel.Application, methodName, request);
            var response = await action();
            MethodLogger.MethodEnded(LoggingTierLevel.Application, methodName, response);
            return response;
        }
    }
}
