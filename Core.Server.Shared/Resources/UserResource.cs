using Newtonsoft.Json;

namespace Core.Server.Shared.Resources.Users
{
    public class UserResource : Resource
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }

        public string FullName => FirstName + " " + LastName;
    }
}
