using Core.Server.Common.Cache;
using Core.Server.Common.Entities;
using Core.Server.Common.Query.Infrastructure;
using Core.Server.Common.Repositories;
using Core.Server.Injection.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Persistence.Cache
{
    [InjectOverrid]
    public class QueryCachedRepository<TEntity>
        : IQueryRepository<TEntity>
        where TEntity : Entity
    {
        private int MAX_CACHED = 3;
        private Dictionary<QueryRequest, IEnumerable<string>> queryRequestToIds;

        [Dependency]
        public IEntityCache<TEntity> Cache;

        [Dependency("QueryRepository")]
        public IQueryRepository<TEntity> QueryRepository;     

        public QueryCachedRepository()
        {
            queryRequestToIds = new Dictionary<QueryRequest, IEnumerable<string>>();
        }

        public async Task<IEnumerable<TEntity>> Query(QueryRequest queryRequest)
        {
            if (queryRequestToIds.ContainsKey(queryRequest))
            {
                var ids = queryRequestToIds[queryRequest];
                return Cache.Get(ids);
            }
            var entities = (await QueryRepository.Query(queryRequest)).ToList();
            AddToCache(queryRequest, entities);
            return entities;
        }

        private void AddToCache(QueryRequest queryRequest, List<TEntity> entities)
        {
            Cache.AddOrSet(entities);
            if (queryRequestToIds.Count > MAX_CACHED)
                queryRequestToIds.Remove(queryRequestToIds.First().Key);
            queryRequestToIds.Add(queryRequest, entities.Select(e => e.Id));
        }
    }
}