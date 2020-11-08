using AutoMapper;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Mappers;
using Core.Server.Shared.Resources;

namespace Core.Server.Application.Mappers.Base
{
    [InjectBoundleWithName]
    public class AutoMapperMapping<TCreateResource, TUpdateResource, TResource, TEntity>
        : IAutoMapperMapping
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
        where TEntity : Entity
    {
        public virtual void AddAutoMapping(Profile profile)
        {
            profile.CreateMap<TCreateResource, TEntity>();
            profile.CreateMap<TUpdateResource, TEntity>();
            profile.CreateMap<TEntity, TResource>();
        }
    }
}
