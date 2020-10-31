using MongoDB.Driver;
using Core.Server.Common.Entities;
using Core.Server.Common.Query;
using Core.Server.Common.Repositories;
using Core.Server.Persistence.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Unity;
using Core.Server.Injection.Attributes;
using Core.Server.Common.Query.Infrastructure;

namespace Core.Server.Persistence.Repositories
{
    [Inject]
    public class QueryRepository<TEntity>
        : BaseRepository<TEntity>,
          IQueryRepository<TEntity>
        where TEntity : Entity
    {
        [Dependency]
        public IQueryFilterFactory QueryFilterFactory;

        public async Task<IEnumerable<TEntity>> Query(QueryRequest queryRequest)
        {
            var filter = QueryFilterFactory.GetFilter<TEntity>(queryRequest.Query);
            var sort = GetSort(queryRequest);
            var options = GetFindOptions(queryRequest, sort);

            var result = await Collection.FindAsync(filter, options);
            return result.ToEnumerable();
        }

        private static FindOptions<TEntity> GetFindOptions(QueryRequest queryRequest, SortDefinition<TEntity> sort)
        {
            return new FindOptions<TEntity>
            {
                Sort = sort,
                Limit = queryRequest.HasPaging ? queryRequest.PageSize : 0,
                Skip = GetSkip(queryRequest),
            };
        }

        private static int GetSkip(QueryRequest queryRequest)
        {
            return queryRequest.HasPaging ? (queryRequest.Page - 1) * queryRequest.PageSize : 0;
        }

        private static SortDefinition<TEntity> GetSort(QueryRequest queryRequest)
        {
            if (!queryRequest.HasOrdering)
                return null;
            if (queryRequest.IsDecending)
                return Builders<TEntity>.Sort.Descending(queryRequest.OrderBy);
            else
                return Builders<TEntity>.Sort.Ascending(queryRequest.OrderBy);
        }
    }
}
