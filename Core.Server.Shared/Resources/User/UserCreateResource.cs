using Core.Server.Shared.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources.Users
{
    public class UserCreateResource : CreateResource
    {
        [Required]
        [EmailAddress]
        [Unchangeable]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        public string Password { get; set; }
    }
}
