using MongoDB.Bson;
using MongoDB.Driver;
using RestApi.Common;
using RestApi.Common.Entities;
using RestApi.Common.Query;
using System.Linq;
using System.Text.RegularExpressions;

namespace RestApi.Persistence.Filters
{
    [Inject]
    public class QueryFilterFactory : IQueryFilterFactory
    {
        public FilterDefinition<TEntity> GetFilter<TEntity>(QueryBase query)
            where TEntity : Entity
        {
            if (query is StringQuery)
                return GetFilter<TEntity>(query as StringQuery);
            else if (query is NumberQuery)
                return GetFilter<TEntity>(query as NumberQuery);
            else if (query is LogicQuery)
                return GetFilter<TEntity>(query as LogicQuery);
            return null;
        }

        private FilterDefinition<TEntity> GetFilter<TEntity>(LogicQuery logicQuery)
            where TEntity : Entity
        {
            var filters = logicQuery.Queries.Select(q => GetFilter<TEntity>(q));
            if (logicQuery.IsAnd)
                return Builders<TEntity>.Filter.And(filters);
            else
                return Builders<TEntity>.Filter.Or(filters);
        }

        private FilterDefinition<TEntity> GetFilter<TEntity>(StringQuery stringQuery)
        {
            var queryExpr = new BsonRegularExpression(new Regex(stringQuery.Regex, RegexOptions.IgnoreCase));
            return Builders<TEntity>.Filter.Regex(stringQuery.Field, queryExpr);
        }

        private FilterDefinition<TEntity> GetFilter<TEntity>(NumberQuery numberQuery)
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
    }
}