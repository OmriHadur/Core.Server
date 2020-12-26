using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources
{
    public class PolicyCreateResource : CreateResource
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ResourceType { get; set; }

        [Required]
        public ResourceActions ResourceActions { get; set; }
    }
}
