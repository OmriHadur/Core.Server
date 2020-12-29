using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources.User
{
    public class UserRoleAlterResource : ChildAlterResource
    {
        [Required]
        public string Id { get; set; }
    }
}
