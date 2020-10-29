using Core.Server.Shared.Query;

namespace Core.Server.Common.Query
{
    public class NumberEntityQuery : QueryEntityBase
    {
        public string Field { get; set; }
        public double Value { get; set; }
        public NumberPropertyQueryOperands Operand { get; set; }
    }
}
