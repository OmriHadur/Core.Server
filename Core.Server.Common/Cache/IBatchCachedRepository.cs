using Core.Server.Common.Entities;

namespace Core.Server.Common.Repositories
{
    public interface IBatchCachedRepository<TEntity> 
        : IBatchRepository<TEntity>
        where TEntity : Entity
    {
    }
}
