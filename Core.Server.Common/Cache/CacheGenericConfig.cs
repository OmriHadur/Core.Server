
using Core.Server.Common.Config;
using Core.Server.Common.Entities;
using System.Linq;
using Unity;

namespace Core.Server.Common.Cache
{
    public class CacheEntityConfig<TEntity>
        : ICacheEntityConfig<TEntity>
        where TEntity:Entity
    {
        [Dependency]
        public CacheConfig CacheConfig;

        public int MaxEntities
        {
            get
            {
                var entityOverride = GetCacheTypeConfig();
                return entityOverride == null ?
                    CacheConfig.MaxEntities :
                    entityOverride.MaxEntities;
            }
        }

        public int MaxQueries
        {
            get
            {
                var entityOverride = GetCacheTypeConfig();
                return entityOverride == null ?
                    CacheConfig.MaxQueries :
                    entityOverride.MaxQueries;
            }
        }

        private CacheTypeConfig GetCacheTypeConfig()
        {
            return CacheConfig.Overrides
                .FirstOrDefault(ovride => ovride.Type == typeof(TEntity).Name);
        }
    }
}
