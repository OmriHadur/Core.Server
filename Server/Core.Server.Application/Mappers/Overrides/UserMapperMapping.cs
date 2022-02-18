using AutoMapper;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Mappers;
using Core.Server.Shared.Resources;

namespace Core.Server.Application.Mappers.Base
{
    [InjectName]
    public class UserMapperMapping : IAutoMapperMapping
    {
        public virtual void AddAutoMapping(Profile profile)
        {
            profile.CreateMap<UserAlterResource, UserEntity>();
            profile.CreateMap<UserEntity, UserResource>()
                .ForMember(r => r.Roles, a => a.Ignore());
            profile.CreateMap<RoleResource, UserRoleResource>();
        }
    }
}