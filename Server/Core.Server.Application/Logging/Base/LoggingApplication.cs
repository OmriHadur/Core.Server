using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using Core.Server.Common.Helpers;
using Core.Server.Common.Logging;
using System;
using System.Linq;
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

        public Task<T> LogginCall<T>(Func<Task<T>> action, object request = null)
        {
            return LogginCall(EntityName, action, LoggingTierLevel.Application, request);
        }

        private string EntityName => Application.GetType().GenericTypeArguments.Last().Name;
    }
}
