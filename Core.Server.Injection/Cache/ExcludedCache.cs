using Core.Server.Common.Cache;
using Core.Server.Common.Entities;
using System;
using System.Collections.Generic;

namespace Core.Server.Injection.Cache
{
    public class ExcludedCache<TEntity>
       : IEntityCache<TEntity>
        where TEntity : Entity
    {

        public event EventHandler<EntityCacheChangedEventArgs> CacheChangedEvent;

        public bool IsAllCached
        {
            get { return false; }
            set { }
        }

        public TEntity Get(string id) => null;

        public IEnumerable<TEntity> Get(IEnumerable<string> ids) => null;

        public IEnumerable<TEntity> GetAll() => null;

        public void AddOrSet(TEntity entity) { }

        public void AddOrSet(IEnumerable<TEntity> entities) { }

        public void Delete(string id) { }

        public void Delete(IEnumerable<string> ids) { }

        public bool IsCached(string id) => false;

        public void Clear() { }

        public bool Any() => false;
    }
}