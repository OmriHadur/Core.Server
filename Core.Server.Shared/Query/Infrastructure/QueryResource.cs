using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Query
{
    [JsonConverter(typeof(QueryResourceConverter))]
    public class QueryResource
    {
        [Required]
        public string Type { get; set; }
    }
}
