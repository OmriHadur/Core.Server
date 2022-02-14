using System.ComponentModel.DataAnnotations;

namespace Core.Server.Common.Entities
{
    public class NamedEntity:Entity
    {
        [Required]
        public string Name { get; set; }
    }
}
