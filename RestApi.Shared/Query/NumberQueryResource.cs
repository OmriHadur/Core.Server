using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RestApi.Shared.Query
{
    public class NumberQueryResource : PropertyQueryResource
    {
        [Required]
        public double Value { get; set; }

        [Required]
        public NumberQueryOperands Operand { get; set; }
    }
}
