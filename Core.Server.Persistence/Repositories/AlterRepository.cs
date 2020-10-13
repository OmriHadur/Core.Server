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
        public async Task Delete(TEntity entity)
        {
            await Collection.DeleteOneAsync(e => e.Id == entity.Id);
        }

        public async Task Remove(string id)
        {
            await Collection.DeleteOneAsync(e => e.Id == id);
        }

        public async Task Add(TEntity entity)
        {
            await Collection.InsertOneAsync(entity);
        }

        public async Task Update(Expression<Func<TEntity, bool>> predicate, Action<TEntity> update)
        {
            var entities = await Collection.FindAsync(predicate);
            await entities.ForEachAsync(update);
        }

        public async Task DeleteMany(Expression<Func<TEntity, bool>> predicate)
        {
            await Collection.DeleteManyAsync(predicate);
        }

        public async Task DeleteOne(Expression<Func<TEntity, bool>> predicate)
        {
            await Collection.DeleteOneAsync(predicate);
        }

        public async Task Update(TEntity entity)
        {
            await Collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
        }
    }
}
