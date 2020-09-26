using MongoDB.Driver;
using Core.Server.Common.Entities;
using Core.Server.Common.Query;

namespace Core.Server.Persistence.Filters
{
    public interface IQueryFilterFactory
    {
        FilterDefinition<TEntity> GetFilter<TEntity>(QueryBase query)
             where TEntity : Entity;
    }
}