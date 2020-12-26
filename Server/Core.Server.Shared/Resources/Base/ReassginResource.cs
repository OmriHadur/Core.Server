
using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources
{
    public class ReassginResource
    {
        [Required]
        public string ResourceId { get; set; }

        [Required]
        public string NewOwnerUserId { get; set; }
    }
}
