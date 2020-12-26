
namespace Core.Server.Shared.Resources
{
    public class RoleResource : Resource
    {
        public string Name { get; set; }

        public PolicyResource[] Policies;
    }
}