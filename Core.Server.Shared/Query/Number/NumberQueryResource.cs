using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Query
{
    public class NumberQueryResource : PropertyQueryResource
    {
        [Required]
        public double Value { get; set; }

        [Required]
        public NumberQueryOperands Operand { get; set; }
    }
}
