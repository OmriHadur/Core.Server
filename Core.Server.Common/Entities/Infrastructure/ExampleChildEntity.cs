using System.ComponentModel.DataAnnotations;

namespace Core.Server.Common.Entities
{
    public class ExampleChildEntity : ChildEntity
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}
