using AutoMapper;
using RestApi.Common;
using RestApi.Common.Entities;
using RestApi.Common.Mapping;
using RestApi.Shared.Resources.Users;

namespace RestApi.Application.Mapping
{
    [InjectMany]
    public class LoginMapping : IResourceMapper
    {
        public void AddMapping(Profile profile)
        {
            profile.CreateMap<LoginCreateResource, LoginEntity>();
            profile.CreateMap<LoginEntity, LoginResource>();
        }
    }
}
