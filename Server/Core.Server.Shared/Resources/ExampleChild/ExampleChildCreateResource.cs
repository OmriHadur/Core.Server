using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources
{
    public class ExampleChildCreateResource : ChildCreateResource
    {
        [Required]
        public string Name { get; set; }
    }
}
