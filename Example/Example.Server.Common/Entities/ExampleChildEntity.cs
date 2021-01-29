using Core.Server.Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace Example.Server.Common.Entities
{
    public class ExampleChildEntity : ChildEntity
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}
