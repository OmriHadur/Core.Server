using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources
{
    public class ChildBatchDeleteResource
    {
        [Required]
        public Dictionary<string,string[]> ParentAndIds { get; set; }
    }
}
