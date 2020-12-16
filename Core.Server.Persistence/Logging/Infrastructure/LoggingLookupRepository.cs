using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Core.Server.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Server.Persistence.Logging
{
    [Inject(2)]
    public class LoggingLookupRepository<TEntity>
        : LoggingRepository<LookupRepository<TEntity>>,
          ILookupRepository<TEntity>
        where TEntity : Entity
    {
        public Task<bool> Any()
        {
            return LogginCall(() => Repository.Any());
        }

        public Task<bool> Exists(string id)
        {
            return LogginCall(() => Repository.Exists(id), id);
        }

        public Task<string> GetNotFoundId(string[] ids)
        {
            return LogginCall(() => Repository.GetNotFoundId(ids), ids);
        }

        public Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return LogginCall(() => Repository.Exists(predicate), predicate);
        }

        public Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return LogginCall(() => Repository.FindAll(predicate), predicate);
        }

        public Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> predicate)
        {
            return LogginCall(() => Repository.FindFirst(predicate), predicate);
        }

        public Task<TEntity> Get(string id)
        {
            return LogginCall(() => Repository.Get(id), id);
        }

        public Task<IEnumerable<TEntity>> Get(string[] ids)
        {
            return LogginCall(() => Repository.Get(ids), ids);
        }

        public Task<IEnumerable<TEntity>> Get()
        {
            return LogginCall(() => Repository.Get());
        }
    }
}
