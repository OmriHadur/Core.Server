using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources
{
    public class ChildUpdateResource : UpdateResource
    {
        [Required]
        public string ParentId { get; set; }
    }
}
