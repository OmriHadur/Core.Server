using Core.Server.Application.Helper;
using Core.Server.Application.Mappers.Base;
using Core.Server.Common;
using Core.Server.Injection.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Entities.Helpers;
using Core.Server.Common.Mappers;
using Core.Server.Common.Repositories;
using Core.Server.Shared.Resources.Users;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Mappers.Implementation
{
    [Inject]
    public class LoginResourceMapper
        : AlterResourceMapper<LoginCreateResource,LoginUpdateResource, LoginResource,LoginEntity>
    {
        [Dependency]
        public IOptions<AppSettings> AppSettings { get; set; }

        [Dependency]
        public IJwtManager JwtManager { get; set; }

        [Dependency]
        public ILookupRepository<UserEntity> UserLookupRepository { get; set; }

        [Dependency]
        public IResourceMapper<UserResource, UserEntity> UserResourceMapper { get; set; }

        public async override Task<LoginEntity> Map(LoginCreateResource resource)
        {
            var userEntity = await UserLookupRepository.FindFirst(e => e.Email == resource.Email);
            return new LoginEntity()
            {
                UserId = userEntity.Id,
                CreateTime = DateTime.Now,
                IsValid = true,
                Token = GetToken(userEntity)
            };
        }

        public override async Task<LoginResource> Map(LoginEntity entity)
        {
            var loginResource = Mapper.Map<LoginResource>(entity);
            var userEntity = await UserLookupRepository.Get(entity.UserId);
            loginResource.User = await UserResourceMapper.Map(userEntity);;
            return loginResource;
        }

        private string GetToken(UserEntity user)
        {
            return JwtManager.GenerateToken(Mapper.Map<UserResource>(user), AppSettings.Value.Secret);
        }
    }
}
