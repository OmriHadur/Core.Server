
using Core.Server.Common.Config;
using Core.Server.Common.Entities;
using System.Linq;
using Unity;

namespace Core.Server.Common.Cache
{
    public class CacheEntityConfig<TEntity>
        : ICacheEntityConfig<TEntity>
        where TEntity : Entity
    {
        private int maxEntities;
        private int maxQueries;

        [Dependency]
        public CacheConfig CacheConfig;

        public int MaxEntities
        {
            get
            {
                if (maxEntities == 0)
                {
                    var entityOverride = GetCacheTypeConfig();
                    maxEntities = entityOverride == null ?
                        CacheConfig.MaxEntities :
                        entityOverride.MaxEntities;
                }
                return maxEntities;
            }
        }

        public int MaxQueries
        {
            get
            {
                if (maxQueries == 0)
                {
                    var entityOverride = GetCacheTypeConfig();
                    maxQueries = entityOverride == null ?
                        CacheConfig.MaxQueries :
                        entityOverride.MaxQueries;
                }
                return maxQueries;
            }
        }

        private CacheTypeConfig GetCacheTypeConfig()
        {
            if (CacheConfig.Overrides == null)
                return null;
            return CacheConfig.Overrides
                .FirstOrDefault(ovride => ovride.Type == typeof(TEntity).Name + nameof(Entity));
        }
    }
}
