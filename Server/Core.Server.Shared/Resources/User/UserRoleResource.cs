namespace Core.Server.Shared.Resources
{
    public class UserRoleResource : ChildResource
    {
        public string Name { get; set; }

        public PolicyResource[] Policies;
    }
}
