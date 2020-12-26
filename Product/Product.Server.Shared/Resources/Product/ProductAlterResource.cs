using Core.Server.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace Product.Server.Shared.Resources.Product
{
    public class ProductAlterResource
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }
    }
}
