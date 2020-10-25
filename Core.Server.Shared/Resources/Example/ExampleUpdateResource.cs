using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources
{
    public class ExampleUpdateResource : UpdateResource
    {
        [Required]
        [Range(0, 1000)]
        public int Value { get; set; }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}
