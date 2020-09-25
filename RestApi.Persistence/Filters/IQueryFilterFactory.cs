using MongoDB.Driver;
using RestApi.Common.Entities;
using RestApi.Common.Query;

namespace RestApi.Persistence.Filters
{
    public interface IQueryFilterFactory
    {
        FilterDefinition<TEntity> GetFilter<TEntity>(QueryBase query)
             where TEntity : Entity;
    }
}