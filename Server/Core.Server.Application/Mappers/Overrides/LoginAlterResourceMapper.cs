using Core.Server.Application.Mappers.Base;
using Core.Server.Common.Attributes;
using Core.Server.Common.Config;
using Core.Server.Common.Entities;
using Core.Server.Common.Entities.Helpers;
using Core.Server.Common.Mappers;
using Core.Server.Common.Repositories;
using Core.Server.Shared.Resources;
using Core.Server.Shared.Resources.User;
using System;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Mappers.Implementation
{
    [Inject]
    public class LoginAlterResourceMapper : AlterResourceMapper<LoginAlterResource, LoginEntity>
    {
        [Dependency]
        public AppSettings AppSettings;

        [Dependency]
        public IJwtManager JwtManager;

        [Dependency]
        public ILookupRepository<UserEntity> UserLookupRepository;

        [Dependency]
        public IResourceMapper<UserResource, UserEntity> UserResourceMapper;

        public async override Task<LoginEntity> MapCreate(LoginAlterResource resource)
        {
            var userEntity = await UserLookupRepository.FindFirst(e => e.Email == resource.Email);
            var userResource = await UserResourceMapper.Map(userEntity);
            return new LoginEntity()
            {
                UserId = userEntity.Id,
                CreateTime = DateTime.Now,
                Token = GetToken(userResource)
            };
        }

        private string GetToken(UserResource userResource)
        {
            return JwtManager.GenerateToken(userResource, AppSettings.Secret);
        }
    }
}
