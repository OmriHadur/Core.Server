using Core.Server.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources
{
    public class UserAlterResource
    {      
        [Immutable]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        public string Password { get; set; }
    }
}
