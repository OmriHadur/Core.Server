using Core.Server.Common.Attributes;
using Core.Server.Common.Cache;
using Core.Server.Common.Config;
using Core.Server.Common.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace Core.Server.Persistence.Cache
{
    [Inject]
    public class EntityCache<TEntity>
       : IEntityCache<TEntity>
        where TEntity : Entity
    {
        [Dependency]
        public ICacheEntityConfig<TEntity> EntityConfig;

        [Dependency]
        public CacheConfig CacheConfig;

        private readonly ConcurrentDictionary<string, TEntity> cache;

        public event EventHandler<EntityCacheChangedEventArgs> CacheChangedEvent;

        public EntityCache()
        {
            cache = new ConcurrentDictionary<string, TEntity>();
        }

        public TEntity Get(string id)
        {
            return cache.ContainsKey(id)
                ? cache[id]
                : null;
        }

        public IEnumerable<TEntity> Get(IEnumerable<string> ids)
        {
            foreach (var id in ids)
                if (cache.ContainsKey(id))
                    yield return Get(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return cache.Values;
        }

        public void AddOrSet(TEntity entity)
        {
            if (entity == null) return;

            cache.AddOrUpdate(entity.Id, s => entity, (s, e) => entity);

            RemoveIfReachedMax();
            CacheChangedEvent?.Invoke(this, new EntityCacheChangedEventArgs() { Id = entity.Id, IsAltered = true });
        }

        private void RemoveIfReachedMax()
        {
            if (cache.Count <= EntityConfig.MaxEntities) return;
            bool removed;
            do
            {
                removed = cache.TryRemove(cache.First().Key, out TEntity entity);

            } while (!removed);
        }

        public void AddOrSet(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                AddOrSet(entity);
        }

        public void Delete(string id)
        {
            if (cache.ContainsKey(id))
            {
                bool removed;
                do
                {
                    removed = cache.TryRemove(id, out TEntity entity);

                } while (!removed);
                CacheChangedEvent?.Invoke(this, new EntityCacheChangedEventArgs() { Id = id, IsDeleted = true });
            }
        }

        public void Delete(IEnumerable<string> ids)
        {
            foreach (var id in ids)
                Delete(id);
        }

        public bool IsCached(string id)
        {
            return cache.ContainsKey(id);
        }

        public void Clear()
        {
            cache.Clear();
            CacheChangedEvent?.Invoke(this, new EntityCacheChangedEventArgs() { IsClear = true });
        }

        public bool Any()
        {
            return cache.Any();
        }
    }
}