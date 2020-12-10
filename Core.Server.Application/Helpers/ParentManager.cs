using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Mappers;
using System.Collections.Generic;
using System.Linq;

namespace Core.Server.Application.Helpers
{
    [Inject]
    public class ParentManager<TParentEntity, TChildEntity>
        : IParentManager<TParentEntity, TChildEntity>
        where TParentEntity : Entity, IParentEntity
        where TChildEntity : Entity
    {
        public void Add(TParentEntity parent, TChildEntity child)
        {
            GetChildren(parent).Add(child);
        }

        public void Add(TParentEntity parent, IEnumerable<TChildEntity> children)
        {
            var parentChildren = GetChildren(parent);
            foreach (var child in children)
                parentChildren.Add(child);
        }

        public bool Exists(TParentEntity parent, string childId)
        {
            return Get(parent, childId) != null;
        }

        public TChildEntity Get(TParentEntity parent, string childId)
        {
            return GetChildren(parent).FirstOrDefault(c => c.Id == childId);
        }

        public void Remove(TParentEntity parent, string childId)
        {
            var children = GetChildren(parent);
            var childEntity = children.FirstOrDefault(c => c.Id == childId);
            children.Remove(childEntity);
        }

        public void RemoveAll(TParentEntity parent)
        {
            GetChildren(parent).Clear();
        }

        public void Replace(TParentEntity parent, TChildEntity child, string childId)
        {
            var children = GetChildren(parent);
            var childEntity = children.FirstOrDefault(c => c.Id == childId);
            children.Remove(childEntity);
            child.Id = childId;
            children.Add(child);
        }

        private IList<TChildEntity> GetChildren(TParentEntity parent)
        {
            return parent.GetChildEntitiess<TChildEntity>();
        }
    }
}
