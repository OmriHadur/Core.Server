using RestApi.Application.Helper;
using RestApi.Common.Applications;
using RestApi.Common.Entities;
using RestApi.Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RestApi.Common;
using RestApi.Shared.Resources.Users;
using Microsoft.Extensions.Options;
using Unity;
using RestApi.Common.Entities.Helpers;
using System;
using System.Collections.Generic;

namespace RestApi.Application
{
    [Inject]
    public class LoginsApplication : RestApplication<LoginCreateResource, LoginResource, LoginEntity>, ILoginsApplication
    {
        private PasswordHasher _passwordHasher = new PasswordHasher();

        [Dependency]
        public IOptions<AppSettings> AppSettings { get; set; }

        [Dependency]
        public IJwtManager JwtManager { get; set; }
        [Dependency]
        public IUsersRepository UsersRepository { get; set; }

        public override async Task<ActionResult<LoginResource>> Get(string id)
        {
            var loginEntity = await Repository.Get(id);
            if (loginEntity == null)
                return NotFound(id);
            var userEntity = await UsersRepository.Get(loginEntity.UserId);
            return GetLoginWithUser(loginEntity, userEntity);
        }

        public async override Task<ActionResult<LoginResource>> Create(LoginCreateResource resource)
        {
            var userEntity = await UsersRepository.GetByEmail(resource.Email);

            if (userEntity == null || !IsPasswordCurrent(resource, userEntity))
                return new UnauthorizedResult();

            await DeleteLastLogin(userEntity);

            var loginEntity = new LoginEntity()
            {
                UserId = userEntity.Id,
                CreateTime = DateTime.Now,
                Token = GetToken(userEntity)
            };
            await AddEntity(loginEntity);
            return GetLoginWithUser(loginEntity, userEntity);
        }

        public async Task DeleteByUserId(string id)
        {
            await (Repository as ILoginsRepository ).DeleteByUserId(id);
        }

        private ActionResult<LoginResource> GetLoginWithUser(LoginEntity loginEntity, UserEntity userEntity)
        {
            var loginResource = Mapper.Map<LoginResource>(loginEntity);
            loginResource.User = Mapper.Map<UserResource>(userEntity);
            return loginResource;
        }

        private async Task DeleteLastLogin(UserEntity user)
        {
            var login = await (Repository as ILoginsRepository ).GetByUserId(user.Id);
            if (login != null)
                await Repository.Delete(login);
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
