using Core.Server.Shared.Resources.User;

namespace Core.Server.Shared.Resources.Users
{
    public class UserResource : Resource
    {
        public string Email { get; set; }

        public UserStatus Status { get; set; }

        public RoleResource[] Roles { get; set; }
    }
}
