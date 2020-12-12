using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Server.Persistence.Repositories
{
    [Inject]
    public class BatchRepository<TEntity>
        : BaseRepository<TEntity>,
          IBatchRepository<TEntity>
        where TEntity : Entity
    {
        public async Task AddMany(IEnumerable<TEntity> entities)
        {
            await Collection.InsertManyAsync(entities);
        }

        public async Task DeleteMany(string[] ids)
        {
            await Collection.DeleteManyAsync(e => ids.Contains(e.Id));
        }

        public async Task<bool> Exists(string[] ids)
        {
            var answer = Collection.AsQueryable().Count(e => ids.Contains(e.Id));
            return answer == ids.Length;
        }

        public async Task ReplaceMany(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                await Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity); 
        }

        public async Task UpdateMany(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                await Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
        }
    }
}
