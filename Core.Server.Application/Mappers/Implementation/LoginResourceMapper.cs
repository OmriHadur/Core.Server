using Core.Server.Application.Helper;
using Core.Server.Application.Mappers.Base;
using Core.Server.Common;
using Core.Server.Common.Entities;
using Core.Server.Common.Entities.Helpers;
using Core.Server.Common.Mappers;
using Core.Server.Common.Repositories;
using Core.Server.Shared.Resources.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application.Mappers.Implementation
{
    public class LoginResourceMapper
        : AlterResourceMapper<LoginCreateResource,LoginUpdateResource, LoginResource,LoginEntity>
    {
        private PasswordHasher _passwordHasher = new PasswordHasher();

        [Dependency]
        public IOptions<AppSettings> AppSettings { get; set; }

        [Dependency]
        public IJwtManager JwtManager { get; set; }

        [Dependency]
        public IQueryRepository<UserEntity> UserQueryRepository { get; set; }

        [Dependency]
        public IResourceMapper<UserResource, UserEntity> UserResourceMapper { get; set; }

        public async override Task<ActionResult<LoginEntity>> Map(LoginCreateResource resource)
        {
            var userEntity = await UserQueryRepository.FindFirst(e => e.Email == resource.Email);
            if (userEntity == null || !IsPasswordCurrent(resource, userEntity))
                return new UnauthorizedResult();

            return new LoginEntity()
            {
                UserId = userEntity.Id,
                CreateTime = DateTime.Now,
                IsValid = true,
                Token = GetToken(userEntity)
            };
        }

        public override async Task<ActionResult<LoginResource>> Map(LoginEntity entity)
        {
            var loginResource = Mapper.Map<LoginResource>(entity);
            var userEntity = await UserQueryRepository.Get(entity.UserId);
            var userResult = await UserResourceMapper.Map(userEntity);
            loginResource.User = userResult.Value;
            return loginResource;
        }

        private string GetToken(UserEntity user)
        {
            return JwtManager.GenerateToken(Mapper.Map<UserResource>(user), AppSettings.Value.Secret);
        }

        private bool IsPasswordCurrent(LoginCreateResource resource, UserEntity user)
        {
            return _passwordHasher.VerifyPasswordHash(resource.Password, user.PasswordHash, user.PasswordSalt);
        }
    }
}
