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
    public class QueryRepository<TEntity> :
        IQueryRepository<TEntity>
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
            get { return GetType().Name.Replace("Repository", string.Empty); }
        }

        public async Task<IEnumerable<TEntity>> FindAll(Expression<Func<TEntity, bool>> predicate)
        {
            return (await Entities.FindAsync(predicate)).ToEnumerable();
        }

        public async Task<TEntity> FindFirst(Expression<Func<TEntity, bool>> predicate)
        {
            return (await Entities.FindAsync(predicate)).FirstOrDefault();
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
        public virtual async Task<IEnumerable<TEntity>> Get()
        {
            var answer = await Entities.FindAsync(e => true);
            return answer.ToEnumerable();
        }

        public async Task<bool> Exists(string id)
        {
            var answer = await Entities.FindAsync(e => e.Id == id);
            return answer.FirstOrDefault() != null;
        }

        public async Task<bool> Exists(Expression<Func<TEntity, bool>> predicate)
        {
            var answer = await Entities.FindAsync(predicate);
            return answer.FirstOrDefault() != null;
        }

        public async Task<IEnumerable<TEntity>> Query(QueryBase query)
        {
            var filter = QueryFilterFactory.GetFilter<TEntity>(query);
            return (await Entities.FindAsync(filter)).ToEnumerable();
        }
    }
}
