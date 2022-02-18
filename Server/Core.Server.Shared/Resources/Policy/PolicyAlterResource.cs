using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources
{
    public class PolicyAlterResource
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ResourceType { get; set; }

        [Required]
        public ResourceActions ResourceActions { get; set; }
    }
}
