using Core.Server.Common;
using Core.Server.Common.Entities;
using Core.Server.Common.Validators;
using Core.Server.Shared.Resources.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Core.Server.Application.Validators.Implementation
{
    [Inject]
    public class LoginValidator
        : ResourceValidator<LoginCreateResource,LoginUpdateResource,LoginEntity>
    {
    }
}
