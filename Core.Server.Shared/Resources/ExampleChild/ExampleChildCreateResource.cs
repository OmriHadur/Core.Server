using Core.Server.Shared.Resources.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources
{
    public class ExampleChildCreateResource : CreateResource, IChildResource
    {
        [Required]
        public string ParentId { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
