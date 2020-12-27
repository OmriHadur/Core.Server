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

        [RequiredOnCreate]
        [MinLength(5)]
        public string Password { get; set; }

        [RequiredOnCreate]
        public string[] RolesIds { get; set; }
    }
}
