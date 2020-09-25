using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RestApi.Shared.Query
{
    public class PropertyQueryResource : QueryResource
    {
        [Required]
        [MinLength(2)]
        public string PropertyName { get; set; }
    }
}
