using Core.Server.Common.Entities;
using Core.Server.Shared.Resources;
using System.Threading.Tasks;

namespace Core.Server.Common.Mappers
{
    public interface IAlterResourceMapper<TAlterResource, TEntity>
        where TEntity : Entity
    {
        Task<TEntity> MapCreate(TAlterResource resource);

        Task MapReplace(TAlterResource resource, TEntity entity);

        Task MapUpdate(TAlterResource resource, TEntity entity);
    }
}
