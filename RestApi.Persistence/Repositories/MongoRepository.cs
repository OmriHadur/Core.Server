using MongoDB.Bson;
using MongoDB.Driver;
using RestApi.Common;
using RestApi.Common.Entities;
using RestApi.Common.Query;
using RestApi.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Authentication;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Unity;

namespace RestApi.Persistence.Repositories
{
    public class MongoRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        protected IMongoCollection<TEntity> Entities;

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
            get{ return GetType().Name.Replace("Repository", ""); }
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

        public async Task<bool> Exists(string id)
        {
            var answer = await Entities.FindAsync(e => e.Id == id);
            return answer.FirstOrDefault() != null;
        }

        public async Task<List<TEntity>> Query(QueryBase query)
        {
            var filter = GetFilter(query);
            return (await Entities.FindAsync(filter)).ToList();
        }

        private static FilterDefinition<TEntity> GetFilter(QueryBase query)
        {
            if (query is StringQuery)
                return GetFilter(query as StringQuery);
            else if(query is NumberQuery)
                return GetFilter(query as NumberQuery);
            else if (query is LogicQuery)
                return GetFilter(query as LogicQuery);
            return null;
        }

        private static FilterDefinition<TEntity> GetFilter(StringQuery stringQuery)
        {
            var queryExpr = new BsonRegularExpression(new Regex(stringQuery.Regex, RegexOptions.IgnoreCase));
            return Builders<TEntity>.Filter.Regex(stringQuery.Field, queryExpr);
        }

        private static FilterDefinition<TEntity> GetFilter(NumberQuery numberQuery)
        {
            switch (numberQuery.Operand)
            {
                case Shared.Query.NumberQueryOperands.LessThen:
                    return Builders<TEntity>.Filter.Lt(numberQuery.Field, numberQuery.Value);
                case Shared.Query.NumberQueryOperands.Equals:
                    return Builders<TEntity>.Filter.Eq(numberQuery.Field, numberQuery.Value);
                case Shared.Query.NumberQueryOperands.GreaterThen:
                    return Builders<TEntity>.Filter.Gt(numberQuery.Field, numberQuery.Value);
            }
            return null;
        }

        private static FilterDefinition<TEntity> GetFilter(LogicQuery logicQuery)
        {
            var filters = logicQuery.Queries.Select(q => GetFilter(q));
            if (logicQuery.IsAnd)
                return Builders<TEntity>.Filter.And(filters);
            else
                return Builders<TEntity>.Filter.Or(filters);
        }
    }
}
