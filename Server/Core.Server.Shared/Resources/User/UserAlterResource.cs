using Core.Server.Shared.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources.Users
{
    public class UserAlterResource
    {      
        [Immutable]
        [EmailAddress]
        public string Email { get; set; }

        [RequiredOnAlter]
        [MinLength(5)]
        public string Password { get; set; }

        [RequiredOnAlter]
        public string[] RolesIds { get; set; }
    }
}
