using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Core.Server.Shared.Resources.Users;
using Core.Server.Common.Applications;

namespace Core.Server.Web.Controllers
{
    public class LoginController :
        BatchController<LoginCreateResource, LoginUpdateResource, LoginResource, IBatchApplication<LoginCreateResource, LoginUpdateResource, LoginResource>>
    {
    }
}