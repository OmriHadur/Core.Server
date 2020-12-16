using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources.Users
{
    public class UserUpdateResource : UpdateResource
    {
        [MinLength(5)]
        public string Password { get; set; }

        public string[] Roles { get; set; }
    }
}
