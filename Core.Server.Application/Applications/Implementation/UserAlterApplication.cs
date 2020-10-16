using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Server.Injection.Attributes;
using Core.Server.Shared.Resources.Users;
using Core.Server.Shared.Errors;
using Unity;

namespace Core.Server.Application
{
    [Inject]
    public class UserAlterApplication
        : AlterApplication<UserCreateResource, UserUpdateResource, UserResource, UserEntity>
    {
        [Dependency]
        public IAlterRepository<LoginEntity> LoginsRepository;

        public async override Task<ActionResult<UserResource>> Create(UserCreateResource createResource)
        {
            if (await QueryRepository.Exists(e => e.Email == createResource.Email))
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