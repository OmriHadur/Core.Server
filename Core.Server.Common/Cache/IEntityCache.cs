using Core.Server.Common.Entities;
using System.Collections.Generic;

namespace Core.Server.Common.Cache
{
    public interface IEntityCache<TEntity>
        where TEntity : Entity
    {
        bool IsCached(string id);

        TEntity Get(string id);

        IEnumerable<TEntity> Get(IEnumerable<string> ids);

        IEnumerable<TEntity> Get();

        void Clear();

        void AddOrSet(TEntity entity);

        void AddOrSet(IEnumerable<TEntity> entities);

        void Delete(string id);

        void Delete(IEnumerable<string> ids);

        bool Any();
    }
}
