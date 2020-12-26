using AutoMapper;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Mappers;
using Core.Server.Shared.Resources;

namespace Core.Server.Application.Mappers.Base
{
    [InjectBoundleWithName]
    public class AutoMapperMapping<TAlterResource, TResource, TEntity>
        : IAutoMapperMapping
        where TResource : Resource
        where TEntity : Entity
    {
        public virtual void AddAutoMapping(Profile profile)
        {
            profile.CreateMap<TAlterResource, TEntity>();
            profile.CreateMap<TEntity, TResource>();
        }
    }
}
