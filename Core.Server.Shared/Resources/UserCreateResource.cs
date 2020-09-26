using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources.Users
{
    public class UserCreateResource : CreateResource
    {
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string FirstName { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(5)]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}
