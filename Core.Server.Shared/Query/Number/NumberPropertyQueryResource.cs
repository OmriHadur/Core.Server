using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Query
{
    public class NumberPropertyQueryResource : PropertyQueryResource
    {
        [Required]
        public double Value { get; set; }

        [Required]
        public NumberPropertyQueryOperands Operand { get; set; }
    }
}
