using Core.Server.Common.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Server.Common.Repositories
{
    public interface IBatchRepository<TEntity>
        : IAlterRepository<TEntity>
        where TEntity : Entity
    {
        Task AddMany(IEnumerable<TEntity> entities);

        Task DeleteMany(string[] ids);
    }
}
