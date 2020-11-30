using Core.Server.Common.Entities;
using Core.Server.Shared.Resources;

namespace Core.Server.Common.Mappers
{
    public interface IAlterChildResourceMapper<TCreateResource, TUpdateResource, TEntity>
        where TCreateResource : ChildCreateResource
        where TUpdateResource : ChildUpdateResource
        where TEntity : Entity
    {
        void Add(TCreateResource resource, TEntity parent);

        void Map(TCreateResource resource, string childId, TEntity parent);

        void Map(TUpdateResource resource, string childId, TEntity parent);

        void Remove(TEntity parent, string childId);

        void RemoveAll(TEntity parent);

        bool Exists(TEntity parent, string childId);
    }
}
