using Core.Server.Application.Helper;
using Core.Server.Common.Applications;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Server.Common;
using Core.Server.Shared.Resources.Users;
using Core.Server.Shared.Errors;
using Unity;

namespace Core.Server.Application
{
    [Inject]
    public class UserApplication :
        BatchApplication<UserCreateResource, UserUpdateResource, UserResource, UserEntity>,
        IUserApplication
    {
        private IUserRepository _usersRepository => QueryRepository as IUserRepository;
        private PasswordHasher _passwordHasher = new PasswordHasher();

        [Dependency]
        public IRestRepository<LoginEntity> LoginsRepository;

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
            if (await _usersRepository.Exists(e => e.Email == createResource.Email))
                return BadRequest(BadRequestReason.SameExists);

            return await base.Create(createResource);
        }

        public async override Task<ActionResult> Delete(string id)
        {
            var respose = await base.Delete(id);
            if (respose is OkResult)
                await LoginsRepository.DeleteMany(e => e.UserId == id);
            return respose;
        }
    }
}