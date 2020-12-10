using System.Collections.Generic;

namespace Core.Server.Common.Entities
{
    public interface IParentEntity
    {
        IList<TEntity> GetChildEntitiess<TEntity>()
            where TEntity : Entity;
    }
}
