using Core.Server.Application.Logging;
using Core.Server.Common.Logging;
using Core.Server.Common.Repositories;
using System;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Persistence.Logging
{
    public abstract class LoggingRepository<TRepository>
        : LoggingCaller
        , IBaseRepository
        where TRepository : IBaseRepository
    {
        [Dependency]
        public TRepository Repository;

        public Task LogginCall(Func<Task> action, object request = null)
        {
            return LogginCall(EntityName, action, LoggingTierLevel.Repository, request);
        }

        public Task<T> LogginCall<T>(Func<Task<T>> action, object request = null)
        {
            return LogginCall(EntityName, action, LoggingTierLevel.Repository, request);
        }

        private string EntityName => Repository.GetType().GenericTypeArguments[0].Name;
    }
}