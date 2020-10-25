using Core.Server.Common.Entities;

namespace Core.Server.Common.Repositories
{
    public interface IAlterCachedRepository<TEntity> 
        : IAlterRepository<TEntity>
        where TEntity : Entity
    {
    }
}
