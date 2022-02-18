using Core.Server.Common.Attributes;
using Core.Server.Common.Cache;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Core.Server.Persistence.Logging;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Persistence.Cache
{
    [Inject(3)]
    public class AlterCachedRepository<TEntity>
        : IAlterRepository<TEntity>
        where TEntity : Entity
    {
        [Dependency]
        public IEntityCache<TEntity> Cache;

        [Dependency]
        public LoggingAlterRepository<TEntity> AlterRepository;

        public async Task Add(TEntity entity)
        {
            await AlterRepository.Add(entity);
            Cache.AddOrSet(entity);
        }

        public async Task DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            await AlterRepository.DeleteMany(predicate);
            Cache.Clear();
        }

        public async Task DeleteOne(Expression<Func<TEntity, bool>> predicate)
        {
            await AlterRepository.DeleteOne(predicate);
            Cache.Clear();
        }

        public async Task Replace(TEntity entity)
        {
            await AlterRepository.Replace(entity);
            Cache.AddOrSet(entity);
        }

        public async Task Update(TEntity entity)
        {
            await AlterRepository.Update(entity);
            Cache.Delete(entity.Id);
        }

        public async Task Delete(string id)
        {
            await AlterRepository.Delete(id);
            Cache.Delete(id);
        }

        public async Task DeleteAll()
        {
            await AlterRepository.DeleteAll();
            Cache.Clear();
        }
    }
}