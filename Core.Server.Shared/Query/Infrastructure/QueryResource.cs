
using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Query
{
    public class QueryResource
    {
        [Required]
        public string Query { get; set; }
    }
    
}
