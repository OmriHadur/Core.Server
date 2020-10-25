using Core.Server.Common.Entities;
using Core.Server.Common.Query;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Common.Repositories
{
    public interface IQueryRepository<TEntity>
        : IBaseRepository
        where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> Query(QueryBase query);
    }
}
