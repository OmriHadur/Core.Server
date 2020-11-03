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
        private Dictionary<QueryRequest, IList<string>> queryRequestToIdsCache;

        [Dependency]
        public IEntityCache<TEntity> Cache;

        [Dependency("QueryRepository")]
        public IQueryRepository<TEntity> QueryRepository;     

        public QueryCachedRepository([Dependency] IEntityCache<TEntity> cache)
        {
            Cache = cache;
            queryRequestToIdsCache = new Dictionary<QueryRequest, IList<string>>();
            Cache.CacheChangedEvent += (s, e) => queryRequestToIdsCache.Clear();
        }

        public async Task<IEnumerable<TEntity>> Query(QueryRequest queryRequest)
        {
            if (queryRequestToIdsCache.ContainsKey(queryRequest))
            {
                var ids = queryRequestToIdsCache[queryRequest];
                var cahcedCount = ids.Where(id => Cache.IsCached(id)).Count();
                if (cahcedCount >= ids.Count() - 1)
                    return Cache.Get(ids);
            }
            var entities = (await QueryRepository.Query(queryRequest)).ToList();
            AddToCache(queryRequest, entities);
            return entities;
        }

        private void AddToCache(QueryRequest queryRequest, List<TEntity> entities)
        {
            Cache.AddOrSet(entities);
            if (queryRequestToIdsCache.Count > MAX_CACHED)
                queryRequestToIdsCache.Remove(queryRequestToIdsCache.First().Key);
            queryRequestToIdsCache.Add(queryRequest, entities.Select(e => e.Id).ToList());
        }
    }
}