using MongoDB.Driver;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Server.Persistence.Repositories
{
    public class RestRepository<TEntity> :
        QueryRepository<TEntity>,
        IRestRepository<TEntity>
        where TEntity : Entity
    {
        public async Task Delete(TEntity entity)
        {
            await Entities.DeleteOneAsync(e => e.Id == entity.Id);
        }

        public async Task Remove(string id)
        {
            await Entities.DeleteOneAsync(e => e.Id == id);
        }

        public async Task Add(TEntity entity)
        {
            await Entities.InsertOneAsync(entity);
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                await Add(entity);
        }

        public async Task Update(Expression<Func<TEntity, bool>> predicate, Action<TEntity> update)
        {
            var entities = await Entities.FindAsync(predicate);
            await entities.ForEachAsync(update);
        }

        public async Task DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            await Entities.DeleteManyAsync(predicate);
        }

        public async Task DeleteOne(Expression<Func<TEntity, bool>> predicate)
        {
            await Entities.DeleteOneAsync(predicate);
        }

        public async Task RemoveRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
                await Delete(entity);
        }

        public async Task Update(TEntity entity)
        {
            await Entities.ReplaceOneAsync(e => e.Id == entity.Id, entity);
        }
    }
}
