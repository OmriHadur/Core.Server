using RestApi.Application.Helper;
using RestApi.Common.Applications;
using RestApi.Common.Entities;
using RestApi.Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using RestApi.Common;
using RestApi.Shared.Resources.Users;
using RestApi.Shared.Errors;
using Unity;
using System.Collections.Generic;

namespace RestApi.Application.Application
{
    [Inject]
    public class UserApplication : RestApplication<UserCreateResource, UserResource, UserEntity>, IUserApplication
    {
        private IUserRepository _usersRepository => Repository as IUserRepository;
        private PasswordHasher _passwordHasher = new PasswordHasher();

        [Dependency]
        public ILoginApplication LoginsApplication;

        protected override UserEntity GetNewTEntity(UserCreateResource resource)
        {
            var userEntity = base.GetNewTEntity(resource);
            byte[] passwordHash, passwordSalt;
            _passwordHasher.CreatePasswordHash(resource.Password, out passwordHash, out passwordSalt);
            userEntity.PasswordHash = passwordHash;
            userEntity.PasswordSalt = passwordSalt;
            return userEntity;
        }

        public async override Task<ActionResult<UserResource>> Create(UserCreateResource createResource)
        {
            if (await _usersRepository.EmailExists(createResource.Email))
                return BadRequest(BadRequestReason.SameExists);

            return await base.Create(createResource);
        }

        public async override Task<ActionResult> Delete(string id)
        {
            var respose = await base.Delete(id);
            if (respose is OkResult)
                await LoginsApplication.DeleteByUserId(id);
            return respose;
        }
    }
}