using Core.Server.Common.Entities;

namespace Core.Server.Common.Cache
{
    public interface ICacheEntityConfig<TEntity>
        where TEntity : Entity
    {
        int MaxEntities { get; }
        int MaxQueries { get; }
    }
}
