namespace Core.Server.Common.Entities
{
    public class RoleEntity : Entity
    {
        public string Name { get; set; }

        public string[] PoliciesId { get; set; }
    }
}
