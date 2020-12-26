using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources
{
    public class ChildAlterResource
    {
        [Required]
        public string ParentId { get; set; }
    }
}
