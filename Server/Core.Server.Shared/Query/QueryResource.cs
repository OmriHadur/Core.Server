using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Query
{
    public class QueryResource
    {
        [Required]
        public string QueryPhrase { get; set; }

        public string OrderBy { get; set; }

        public string OrderByDescending { get; set; }

        [Range(0,1000)]
        public int Page { get; set; }

        [Range(0, 1000)]
        public int PageSize { get; set; }
    }
    
}
