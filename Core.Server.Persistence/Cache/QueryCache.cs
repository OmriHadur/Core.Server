using Core.Server.Common.Cache;
using Core.Server.Common.Entities;
using Core.Server.Common.Query.Infrastructure;
using Core.Server.Common.Attributes;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace Core.Server.Persistence.Cache
{
    [Inject]
    public class QueryCache<TEntity>
        : IQueryCache<TEntity>
        where TEntity : Entity
    {
        private Dictionary<QueryRequest, IList<string>> queryRequestToIdsCache;

        [Dependency]
        public ICacheEntityConfig<TEntity> EntityConfig;

        public IEntityCache<TEntity> Cache;

        public QueryCache([Dependency] IEntityCache<TEntity> cache)
        {
            Cache = cache;
            queryRequestToIdsCache = new Dictionary<QueryRequest, IList<string>>();
            Cache.CacheChangedEvent += (s, e) => queryRequestToIdsCache.Clear();
        }

        public IEnumerable<TEntity> GetEntities(QueryRequest queryRequest)
        {
            if (queryRequestToIdsCache.ContainsKey(queryRequest))
            {
                var ids = queryRequestToIdsCache[queryRequest];
                var cahcedCount = ids.Where(id => Cache.IsCached(id)).Count();
                if (cahcedCount >= ids.Count() - 1)
                    return Cache.Get(ids);
            }
            return null;
        }

        public void Add(QueryRequest queryRequest, IEnumerable<TEntity> entities)
        {
            Cache.AddOrSet(entities);
            if (queryRequestToIdsCache.Count > EntityConfig.MaxQueries)
                queryRequestToIdsCache.Remove(queryRequestToIdsCache.First().Key);
            queryRequestToIdsCache.Add(queryRequest, entities.Select(e => e.Id).ToList());
        }
    }
}
