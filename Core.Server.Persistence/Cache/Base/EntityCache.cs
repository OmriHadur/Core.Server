using Core.Server.Common.Cache;
using Core.Server.Common.Config;
using Core.Server.Common.Entities;
using Core.Server.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;
using System.Collections.Concurrent;

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

        public bool IsAllCached { get; set; }

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
                yield return Get(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return cache.Values;
        }

        public void AddOrSet(TEntity entity)
        {
            if (entity == null) return;
            if (cache.ContainsKey(entity.Id))
                cache[entity.Id] = entity;
            else
            {
                cache.Add(entity.Id, entity);
                if (cache.Count > EntityConfig.MaxEntities)
                    cache.Remove(cache.First().Key);
            }
            CacheChangedEvent?.Invoke(this, new EntityCacheChangedEventArgs() { Id = entity.Id, IsAltered = true });
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
                cache.Remove(id);
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
            IsAllCached = false;
            cache.Clear();
            CacheChangedEvent?.Invoke(this, new EntityCacheChangedEventArgs() { IsClear = true });
        }

        public bool Any()
        {
            return cache.Any();
        }
    }
}