﻿using Core.Server.Common.Attributes;
using Core.Server.Common.Cache;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Core.Server.Persistence.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Persistence.Cache
{
    [Inject(3)]
    public class LookupCachedRepository<TEntity>
        : ILookupRepository<TEntity>
        where TEntity : Entity
    {
        [Dependency]
        public IEntityCache<TEntity> Cache;

        [Dependency]
        public LoggingLookupRepository<TEntity> LookupRepository;

        public async Task<bool> Any()
        {
            if (Cache.Any())
                return true;

            return await LookupRepository.Any();
        }

        public async Task<bool> Exists(string id)
        {
            if (Cache.IsCached(id))
                return true;

            return await LookupRepository.Exists(id);
        }

        public async Task<IEnumerable<string>> GetNotFoundIds(string[] ids)
        {
            var cachedIds = Cache.Get(ids);
            if (cachedIds.Count() == ids.Length && cachedIds.All(e => e != null))
                return null;
            return await LookupRepository.GetNotFoundIds(ids);
        }

        public Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return LookupRepository.Exists(predicate);
        }

        public async Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = await LookupRepository.FindAll(predicate);
            Cache.AddOrSet(entities);
            return entities;
        }

        public async Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = await LookupRepository.FindFirst(predicate);
            Cache.AddOrSet(entity);
            return entity;
        }

        public async Task<TEntity> Get(string id)
        {
            var cachedEntity = Cache.Get(id);
            if (cachedEntity != null)
                return cachedEntity;

            var entity = await LookupRepository.Get(id);
            Cache.AddOrSet(entity);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> Get(string[] ids)
        {
            var entitiesHave = Cache.Get(ids);
            var idsNotHave = ids.Except(entitiesHave.Select(e => e?.Id)).ToArray();
            if (idsNotHave.Count() == 0)
                return entitiesHave;

            var entities = await LookupRepository.Get(idsNotHave);
            Cache.AddOrSet(entities);
            return entities.Union(entitiesHave);
        }

        public async Task<IEnumerable<TEntity>> Get()
        {
            var entities = await LookupRepository.Get();
            Cache.AddOrSet(entities);
            return entities;
        }
    }
}