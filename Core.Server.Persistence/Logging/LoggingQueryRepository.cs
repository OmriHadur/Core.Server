using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Query.Infrastructure;
using Core.Server.Common.Repositories;
using Core.Server.Persistence.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Persistence.Logging
{
    [Inject(2)]
    public class LoggingQueryRepository<TEntity>
        : LoggingRepository<QueryRepository<TEntity>>,
          IQueryRepository<TEntity>
        where TEntity : Entity
    {
        public Task<IEnumerable<TEntity>> Query(QueryRequest queryRequest)
        {
            return LogginCall(() => Repository.Query(queryRequest), queryRequest);
        }
    }
}
