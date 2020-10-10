using Core.Server.Common.Entities;
using Core.Server.Shared.Resources;
using System.Threading.Tasks;

namespace Core.Server.Common.Mappers
{
    public interface IAlterResourceMapper<TCreateResource, TUpdateResource, TResource, TEntity>
        : IResourceMapper<TResource, TEntity>
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
        where TEntity : Entity, new()
    {
        Task<TEntity> Map(TCreateResource resource);

        Task<TEntity> Map(TUpdateResource resource, TEntity entity);
    }
}
