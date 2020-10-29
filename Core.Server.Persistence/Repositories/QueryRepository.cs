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

        public async Task<IEnumerable<TEntity>> Query(QueryEntityBase query)
        {
            var filter = QueryFilterFactory.GetFilter<TEntity>(query);
            return (await Collection.FindAsync(filter)).ToEnumerable();
        }
    }
}
