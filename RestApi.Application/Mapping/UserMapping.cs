using AutoMapper;
using RestApi.Common;
using RestApi.Common.Entities;
using RestApi.Common.Mapping;
using RestApi.Shared.Resources.Users;

namespace RestApi.Application.Mapping
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
