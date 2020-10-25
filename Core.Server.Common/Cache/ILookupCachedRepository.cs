using Core.Server.Common.Entities;

namespace Core.Server.Common.Repositories
{
    public interface ILookupCachedRepository<TEntity> 
        : ILookupRepository<TEntity>
        where TEntity : Entity
    {
        void Set(TEntity entity);

        void Set(TEntity[] entities);
    }
}
