using MongoDB.Driver;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Server.Common.Attributes;

namespace Core.Server.Persistence.Repositories
{
    [Inject]
    public class AlterRepository<TEntity>
        : BaseRepository<TEntity>,
          IAlterRepository<TEntity>
        where TEntity : Entity
    {
        public Task Delete(string id)
        {
            return Collection.DeleteOneAsync(e => e.Id == id);
        }

        public Task DeleteAll()
        {
            return Collection.DeleteManyAsync(e => true);
        }

        public Task Remove(string id)
        {
            return Collection.DeleteOneAsync(e => e.Id == id);
        }

        public Task Add(TEntity entity)
        {
            return Collection.InsertOneAsync(entity);
        }

        public async Task Update(Expression<Func<TEntity, bool>> predicate, Action<TEntity> update)
        {
            var entities = await Collection.FindAsync(predicate);
            await entities.ForEachAsync(update);
        }

        public Task DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.DeleteManyAsync(predicate);
        }

        public Task DeleteOne(Expression<Func<TEntity, bool>> predicate)
        {
            return Collection.DeleteOneAsync(predicate);
        }

        public Task Update(TEntity entity)
        {
            return Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
        }

        public Task Replace(TEntity entity)
        {
            return Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
        }
    }
}