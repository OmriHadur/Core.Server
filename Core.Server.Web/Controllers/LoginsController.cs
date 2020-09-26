using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Core.Server.Shared.Resources.Users;

namespace Core.Server.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginsController : RestController<LoginCreateResource, LoginResource>
    {
        public async override Task<ActionResult<LoginResource>> Create(LoginCreateResource createResource)
        {
            var resource = await base.Create(createResource);
            Response.Headers.Add("Authorization", "Bearer " + resource.Value?.Token);
            return resource;
        }
    }
}
