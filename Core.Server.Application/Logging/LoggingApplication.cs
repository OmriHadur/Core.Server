using Core.Server.Common.Applications;
using Core.Server.Common.Helpers;
using Core.Server.Common.Logging;
using Core.Server.Shared.Resources.Users;
using System;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Logging
{
    public abstract class LoggingApplication<TApplication>
        : LoggingCaller
        , IBaseApplication
        where TApplication : IBaseApplication
    {
        [Dependency]
        public TApplication Application;

        public ICurrentUserGetter CurrentUserGetter { get; set; }

        public Task<T> LogginCall<T>(Func<Task<T>> action, object request)
        {
            return LogginCall(action, LoggingTierLevel.Application, request);
        }

        public Task<T> LogginCall<T>(Func<Task<T>> action)
        {
            return LogginCall(action, LoggingTierLevel.Application);
        }
    }
}
