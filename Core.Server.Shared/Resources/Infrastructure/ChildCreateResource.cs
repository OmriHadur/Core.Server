using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources
{
    public class ChildCreateResource : CreateResource
    {
        [Required]
        public string ParentId { get; set; }
    }
}
