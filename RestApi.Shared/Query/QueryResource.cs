using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RestApi.Shared.Query
{
    [JsonConverter(typeof(QueryResourceConverter))]
    public class QueryResource
    {
        [Required]
        public string Type { get; set; }
    }
}
