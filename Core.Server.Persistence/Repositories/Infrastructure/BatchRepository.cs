using MongoDB.Driver;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Core.Server.Common.Attributes;

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
            var updates = new List<WriteModel<TEntity>>();
            foreach (var entity in entities)
                updates.Add(new ReplaceOneModel<TEntity>("{ _id: " + entity.Id + "}", entity));
            await Collection.BulkWriteAsync(updates, new BulkWriteOptions() { IsOrdered = false });
        }

        public async Task UpdateMany(IEnumerable<TEntity> entities)
        {
            //TODO UpdateMany
            //await Collection.UpdateManyAsync(entities);
        }
    }
}
