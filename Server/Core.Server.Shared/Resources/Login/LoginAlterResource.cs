using Core.Server.Shared.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources.User
{
    public class LoginAlterResource 
    {
        [EmailAddress]
        [Immutable]
        public string Email { get; set; }

        [Immutable]
        public string Password { get; set; }
    }
}
