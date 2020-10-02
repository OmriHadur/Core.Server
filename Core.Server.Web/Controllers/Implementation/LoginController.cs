using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Core.Server.Shared.Resources.Users;

namespace Core.Server.Web.Controllers
{
    public class LoginController :
        RestController<LoginCreateResource, LoginUpdateResource, LoginResource>
    {
    }
}