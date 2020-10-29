using MongoDB.Bson;
using MongoDB.Driver;
using Core.Server.Common.Entities;
using Core.Server.Common.Query;
using System.Linq;
using System.Text.RegularExpressions;
using Core.Server.Injection.Attributes;

namespace Core.Server.Persistence.Filters
{
    [Inject]
    public class QueryFilterFactory : IQueryFilterFactory
    {
        public FilterDefinition<TEntity> GetFilter<TEntity>(QueryEntityBase query)
            where TEntity : Entity
        {
            if (query is StringEntityQuery)
                return GetFilter<TEntity>(query as StringEntityQuery);
            else if (query is NumberEntityQuery)
                return GetFilter<TEntity>(query as NumberEntityQuery);
            else if (query is LogicEntityQuery)
                return GetFilter<TEntity>(query as LogicEntityQuery);
            return null;
        }

        private FilterDefinition<TEntity> GetFilter<TEntity>(LogicEntityQuery logicQuery)
            where TEntity : Entity
        {
            var filters = logicQuery.Queries.Select(q => GetFilter<TEntity>(q));
            if (logicQuery.IsAnd)
                return Builders<TEntity>.Filter.And(filters);
            else
                return Builders<TEntity>.Filter.Or(filters);
        }

        private FilterDefinition<TEntity> GetFilter<TEntity>(StringEntityQuery stringQuery)
        {
            var queryExpr = new BsonRegularExpression(new Regex(stringQuery.Regex, RegexOptions.IgnoreCase));
            return Builders<TEntity>.Filter.Regex(stringQuery.Field, queryExpr);
        }

        private FilterDefinition<TEntity> GetFilter<TEntity>(NumberEntityQuery numberQuery)
        {
            switch (numberQuery.Operand)
            {
                case Shared.Query.NumberPropertyQueryOperands.LessThen:
                    return Builders<TEntity>.Filter.Lt(numberQuery.Field, numberQuery.Value);
                case Shared.Query.NumberPropertyQueryOperands.Equals:
                    return Builders<TEntity>.Filter.Eq(numberQuery.Field, numberQuery.Value);
                case Shared.Query.NumberPropertyQueryOperands.GreaterThen:
                    return Builders<TEntity>.Filter.Gt(numberQuery.Field, numberQuery.Value);
                default:
                    return null;
            }
        }
    }
}