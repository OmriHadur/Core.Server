using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources
{
    public class RoleCreateResource : CreateResource
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string[] PoliciesId;
    }
}
