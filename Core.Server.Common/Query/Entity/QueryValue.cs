
namespace Core.Server.Common.Query
{
    public class QueryValue<TValue,TOperand> 
        : QueryField
    {
        public TValue Value { get; set; }
        public TOperand Operand { get; set; }
    }
}
