namespace Core.Server.Shared.Resources.User
{
    public class UserResource : Resource
    {
        public string Email { get; set; }

        public RoleResource[] Roles { get; set; }
    }
}
