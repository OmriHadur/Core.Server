using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources.Users
{
    public class UserUpdateResource : UpdateResource
    {
        [MinLength(3)]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [MinLength(3)]
        [MaxLength(25)]
        public string LastName { get; set; }

        [MinLength(5)]
        public string Password { get; set; }
    }
}
