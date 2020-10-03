using AutoMapper;
using Core.Server.Common.Entities;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc;
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
        void AddAutoMapping(Profile profile);

        Task<ActionResult<TEntity>> Map(TCreateResource resource);

        Task<ActionResult<TEntity>> Map(TUpdateResource resource, TEntity entity);
    }
}
