using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources
{
    public class ExampleChildAlterResource : ChildAlterResource
    {
        [Required]
        public string Name { get; set; }
    }
}
