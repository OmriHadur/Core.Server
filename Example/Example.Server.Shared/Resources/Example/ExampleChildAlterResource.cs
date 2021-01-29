using Core.Server.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace Example.Server.Shared.Resources
{
    public class ExampleChildAlterResource : ChildAlterResource
    {
        [Required]
        public string Name { get; set; }
    }
}
