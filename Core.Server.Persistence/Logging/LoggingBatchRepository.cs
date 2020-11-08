using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Core.Server.Persistence.Cache;
using Core.Server.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Server.Persistence.Logging
{
    [Inject(2)]
    public class LoggingBatchRepository<TEntity>
        : LoggingRepository<BatchRepository<TEntity>>,
          IBatchRepository<TEntity>
        where TEntity : Entity
    {
        public Task AddMany(IEnumerable<TEntity> entities)
        {
            return LogginCall(() => Repository.AddMany(entities), entities);
        }

        public Task DeleteMany(string[] ids)
        {
            return LogginCall(() => Repository.DeleteMany(ids), ids);
        }
    }
}
