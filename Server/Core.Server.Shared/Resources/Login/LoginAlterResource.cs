using Core.Server.Shared.Attributes;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources.Users
{
    public class LoginAlterResource 
    {
        [Required]
        [EmailAddress]
        [Immutable]
        public string Email { get; set; }

        [Required]
        [Immutable]
        public string Password { get; set; }
    }
}
