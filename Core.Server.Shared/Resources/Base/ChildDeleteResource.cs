using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources
{
    public class ChildDeleteResource
    {
        [Required]
        public string ParentId { get; set; }
    }
}
