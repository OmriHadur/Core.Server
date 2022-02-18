using Core.Server.Common.Entities;
using Core.Server.Common.Query;
using MongoDB.Driver;

namespace Core.Server.Persistence.Filters
{
    public interface IQueryFilterFactory
    {
        FilterDefinition<TEntity> GetFilter<TEntity>(QueryBase query)
             where TEntity : Entity;
    }
}