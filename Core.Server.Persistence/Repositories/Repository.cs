using MongoDB.Driver;
using Core.Server.Common;
using Core.Server.Common.Entities;
using Core.Server.Common.Query;
using Core.Server.Common.Repositories;
using Core.Server.Persistence.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Authentication;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Persistence.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        protected IMongoCollection<TEntity> Entities;

        [Dependency]
        public IQueryFilterFactory QueryFilterFactory;
        
        [Dependency]
        public IMongoDBConfig MongoDatabaseSettings
        {
            set
            {
                var settings = MongoClientSettings.FromUrl(new MongoUrl(value.ConnectionString));
                settings.SslSettings =
                    new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                var client = new MongoClient(settings);
                var database = client.GetDatabase(value.Database);
                Entities = database.GetCollection<TEntity>(CollectionName);
            }
        }

        private string CollectionName
        {
            get{ return GetType().Name.Replace("Repository", string.Empty); }
        }

        public async Task Delete(TEntity entity) {
            await Entities.DeleteOneAsync(e => e.Id == entity.Id);
        }

        public async Task Remove(string id) {
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

        public async Task<List<TEntity>> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return (await Entities.FindAsync(predicate)).ToList();
        }

        public async Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> predicate)
        {
            return (await Entities.FindAsync(predicate)).FirstOrDefault();
        }

        public async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            return (await Entities.FindAsync(predicate)).Any();
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
        
        public async Task<TEntity> Get(string id)
        {
            return (await Entities.FindAsync(e => e.Id == id)).FirstOrDefault();
        }

        public async Task<IEnumerable<TEntity>> GetAll(IEnumerable<string> ids)
        {
            return (await Entities.FindAsync(e => ids.Contains(e.Id))).ToList();
        }

        public async Task<IEnumerable<TEntity>> FindAll(Func<TEntity, bool> findFunc)
        {
            return (await Entities.FindAsync(e => findFunc(e))).ToList();
        }
        public virtual async Task<List<TEntity>> Get()
        {
            var answer = await Entities.FindAsync(e => true);
            return answer.ToList();
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

        public async Task<bool> IsExists(string id)
        {
            var answer = await Entities.FindAsync(e => e.Id == id);
            return answer.FirstOrDefault() != null;
        }

        public async Task<bool> IsExists(Expression<Func<TEntity, bool>> predicate)
        {
            var answer = await Entities.FindAsync(predicate);
            return answer.FirstOrDefault() != null;
        }

        public async Task<List<TEntity>> Query(QueryBase query)
        {
            var filter = QueryFilterFactory.GetFilter<TEntity>(query);
            return (await Entities.FindAsync(filter)).ToList();
        }
    }
}
