using RestApi.Shared.Query;

namespace RestApi.Common.Query
{
    public class NumberQuery : QueryBase
    {
        public string Field { get; set; }
        public double Value { get; set; }
        public NumberQueryOperands Operand { get; set; }
    }
}
