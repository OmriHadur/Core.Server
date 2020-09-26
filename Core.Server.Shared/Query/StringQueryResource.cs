using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Query
{
    public class StringQueryResource : PropertyQueryResource
    {
        [Required]
        [MinLength(1)]
        public string Value { get; set; }

        [Required]
        public StringQueryOperands Operand { get; set; }
    }
}
