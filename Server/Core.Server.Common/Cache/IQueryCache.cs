using Core.Server.Common.Entities;
using Core.Server.Common.Query.Infrastructure;
using System.Collections.Generic;

namespace Core.Server.Common.Cache
{
    public interface IQueryCache<TEntity> where TEntity : Entity
    {
        void Add(QueryRequest queryRequest, IEnumerable<TEntity> entities);
        IEnumerable<TEntity> GetEntities(QueryRequest queryRequest);
    }
}