using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Query
{
    public class QueryResource
    {
        [Required]
        public string QueryPhrase { get; set; }

        public string OrderBy { get; set; }

        public string OrderByDecending { get; set; }

        [Range(0,1000)]
        public string Page { get; set; }

        [Range(0, 1000)]
        public string PageSize { get; set; }
    }
    
}
