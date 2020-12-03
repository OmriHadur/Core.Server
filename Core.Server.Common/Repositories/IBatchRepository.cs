using Core.Server.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Common.Repositories
{
    public interface IBatchRepository<TEntity>
        : IBaseRepository
        where TEntity : Entity
    {
        Task AddMany(IEnumerable<TEntity> entities);

        Task UpdateMany(IEnumerable<TEntity> entities);

        Task DeleteMany(string[] ids);

        Task<bool> Exists(string[] ids);
    }
}
