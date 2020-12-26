using Core.Server.Common.Attributes;
using Core.Server.Common.Config;
using Core.Server.Common.Entities;
using Core.Server.Common.Entities.Helpers;
using Core.Server.Shared.Resources;
using Core.Server.Shared.Resources.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Unity;

namespace Core.Server.Application
{
    [Inject]
    public class LoginAlterApplication: AlterApplication<LoginAlterResource, LoginResource, LoginEntity>
    {
        [Dependency]
        public AppSettings AppSettings;

        [Dependency]
        public IJwtManager JwtManager;

        public override async Task<ActionResult<LoginResource>> Create(LoginAlterResource resource)
        {
            if (resource.Email == AppSettings.AdminUserName &&
                resource.Password == AppSettings.AdminUserPassword)
            {
                var adminUser = new UserResource() { Email = AppSettings.AdminUserName };
                return new LoginResource()
                {
                    Token = JwtManager.GenerateToken(adminUser, AppSettings.Secret),
                    User = adminUser
                };
            }
            return await base.Create(resource);
        }
    }
}