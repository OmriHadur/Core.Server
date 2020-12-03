using Core.Server.Common.Cache;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Core.Server.Common.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;
using Core.Server.Persistence.Logging;

namespace Core.Server.Persistence.Cache
{
    [Inject(3)]
    public class BatchCachedRepository<TEntity>
        : IBatchRepository<TEntity>
        where TEntity : Entity
    {
        [Dependency]
        public IEntityCache<TEntity> Cache;

        [Dependency]
        public LoggingBatchRepository<TEntity> BatchRepository;

        public async Task AddMany(IEnumerable<TEntity> entities)
        {
            await BatchRepository.AddMany(entities);
            Cache.AddOrSet(entities);
        }

        public async Task DeleteMany(string[] ids)
        {
            await BatchRepository.DeleteMany(ids);
            Cache.Delete(ids);
        }

        public Task<bool> Exists(string[] ids)
        {
            return BatchRepository.Exists(ids);
        }

        public async Task UpdateMany(IEnumerable<TEntity> entities)
        {
            await BatchRepository.UpdateMany(entities);
            Cache.AddOrSet(entities);
        }
    }
}