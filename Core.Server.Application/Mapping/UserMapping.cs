using AutoMapper;
using Core.Server.Common;
using Core.Server.Common.Entities;
using Core.Server.Common.Mapping;
using Core.Server.Shared.Resources.Users;

namespace Core.Server.Application.Mapping
{
    [InjectMany]
    public class UserMapping : IResourceMapper
    {
        public void AddMapping(Profile profile)
        {
            profile.CreateMap<UserCreateResource, UserEntity>();
            profile.CreateMap<UserEntity, UserResource>();
        }
    }
}
