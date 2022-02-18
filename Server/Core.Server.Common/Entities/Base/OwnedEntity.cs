using System.ComponentModel.DataAnnotations;

namespace Core.Server.Common.Entities
{
    public class OwnedEntity : Entity
    {
        [Required]
        public string UserId { get; set; }
    }
}
