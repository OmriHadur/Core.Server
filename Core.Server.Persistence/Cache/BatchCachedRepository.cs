using Core.Server.Common.Cache;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Core.Server.Injection.Attributes;
using Core.Server.Persistence.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Persistence.Cache
{
    [Inject(2)]
    public class BatchCachedRepository<TEntity>
        : IBatchRepository<TEntity>
        where TEntity : Entity
    {
        [Dependency]
        public IEntityCache<TEntity> Cache;

        [Dependency]
        public BatchRepository<TEntity> BatchRepository;

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
    }
}