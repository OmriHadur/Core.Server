using Core.Server.Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace Product.Server.Common.Entities
{
    public class ProductEntity : Entity
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}
