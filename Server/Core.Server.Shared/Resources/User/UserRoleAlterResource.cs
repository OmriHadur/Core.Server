using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources
{
    public class UserRoleAlterResource : ChildAlterResource
    {
        [Required]
        public string Id { get; set; }
    }
}
