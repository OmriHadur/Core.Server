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
        public FilterDefinition<TEntity> GetFilter<TEntity>(QueryBase query)
            where TEntity : Entity
        {
            if (query is QueryString)
                return GetFilter<TEntity>(query as QueryString);
            else if (query is QueryNumber)
                return GetFilter<TEntity>(query as QueryNumber);
            else if (query is QueryUnion)
                return GetFilter<TEntity>(query as QueryUnion);
            return null;
        }

        private FilterDefinition<TEntity> GetFilter<TEntity>(QueryUnion queryUnion)
            where TEntity : Entity
        {
            var filters = queryUnion.Queries.Select(q => GetFilter<TEntity>(q));
            if (queryUnion.Operand == QueryUnionOperands.And)
                return Builders<TEntity>.Filter.And(filters);
            else
                return Builders<TEntity>.Filter.Or(filters);
        }

        private FilterDefinition<TEntity> GetFilter<TEntity>(QueryString queryString)
        {
            var regex = GetRegex(queryString);

            var queryExpr = new BsonRegularExpression(new Regex(regex, RegexOptions.IgnoreCase));
            return Builders<TEntity>.Filter.Regex(queryString.Field, queryExpr);
        }

        private static string GetRegex(QueryString queryString)
        {
            var regex = queryString.Value;
            switch (queryString.Operand)
            {
                case QueryStringOperands.Starts:
                    return '^' + regex;
                case QueryStringOperands.Ends:
                    return regex + "$";
                case QueryStringOperands.Empty:
                    return "^$";
                case QueryStringOperands.NotEmpty:
                    return "$";
            }
            return regex;
        }

        private FilterDefinition<TEntity> GetFilter<TEntity>(QueryNumber queryNumber)
        {
            switch (queryNumber.Operand)
            {
                case QueryNumberOperands.LessThen:
                    return Builders<TEntity>.Filter.Lt(queryNumber.Field, queryNumber.Value);
                case QueryNumberOperands.LessThenOrEquals:
                    return Builders<TEntity>.Filter.Lte(queryNumber.Field, queryNumber.Value);
                case QueryNumberOperands.Equals:
                    return Builders<TEntity>.Filter.Eq(queryNumber.Field, queryNumber.Value);
                case QueryNumberOperands.GreaterThen:
                    return Builders<TEntity>.Filter.Gt(queryNumber.Field, queryNumber.Value);
                case QueryNumberOperands.GreaterThenOrEquals:
                    return Builders<TEntity>.Filter.Gte(queryNumber.Field, queryNumber.Value);
                default:
                    return null;
            }
        }
    }
}