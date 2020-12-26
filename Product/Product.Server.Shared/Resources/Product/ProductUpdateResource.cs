using Core.Server.Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace Product.Server.Shared.Resources.Product
{
    public class ProductUpdateResource: UpdateResource
    {
        [MinLength(3)]
        public string Name { get; set; }
    }
}
