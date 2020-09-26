using AutoMapper;
using Core.Server.Common;
using Core.Server.Common.Entities;
using Core.Server.Common.Mapping;
using Core.Server.Shared.Resources.Users;

namespace Core.Server.Application.Mapping
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
