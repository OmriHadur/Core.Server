using Core.Server.Common.Attributes;
using Core.Server.Common.Cache;
using Core.Server.Common.Entities;
using Core.Server.Common.Query.Infrastructure;
using Core.Server.Common.Repositories;
using Core.Server.Persistence.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Persistence.Cache
{
    [Inject(3)]
    public class QueryCachedRepository<TEntity>
        : IQueryRepository<TEntity>
        where TEntity : Entity
    {
        [Dependency]
        public LoggingQueryRepository<TEntity> QueryRepository;

        [Dependency]
        public IQueryCache<TEntity> QueryCache;

        public async Task<IEnumerable<TEntity>> Query(QueryRequest queryRequest)
        {
            var entities = QueryCache.GetEntities(queryRequest);
            if (entities != null)
                return entities;
            entities = (await QueryRepository.Query(queryRequest)).ToList();
            QueryCache.Add(queryRequest, entities);
            return entities;
        }
    }
}