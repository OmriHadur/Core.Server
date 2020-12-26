using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources
{
    public class RoleAlterResource
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string[] PoliciesId;
    }
}
