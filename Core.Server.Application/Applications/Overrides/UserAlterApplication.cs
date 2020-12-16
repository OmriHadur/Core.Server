using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Core.Server.Shared.Errors;
using Core.Server.Shared.Resources.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application
{
    [Inject]
    public class UserAlterApplication
        : AlterApplication<UserCreateResource, UserUpdateResource, UserResource, UserEntity>
    {
        [Dependency]
        public IAlterRepository<LoginEntity> LoginsRepository;


        public async override Task<ActionResult> Delete(string id)
        {
            var respose = await base.Delete(id);
            if (respose is OkResult)
                await LoginsRepository.DeleteMany(e => e.UserId == id);
            return respose;
        }
    }
}