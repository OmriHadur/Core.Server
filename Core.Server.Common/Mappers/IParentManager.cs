using Core.Server.Common.Entities;

namespace Core.Server.Common.Mappers
{
    public interface IParentManager<TParentEntity, TChildEntity>
        where TParentEntity : Entity
        where TChildEntity : Entity
    {
        void Add(TParentEntity parent, TChildEntity child);

        void Replace(TParentEntity parent, TChildEntity child, string childId);

        void Remove(TParentEntity parent, string childId);

        void RemoveAll(TParentEntity parent);

        bool Exists(TParentEntity parent, string childId);
        TChildEntity Get(TParentEntity parent, string id);
    }
}
