
namespace Core.Server.Common.Query
{
    public class QueryValue<TValue, TOperand>
        : QueryField
    {
        public TValue Value { get; set; }
        public TOperand Operand { get; set; }

        public override bool Equals(object obj)
        {
            var queryValue = obj as QueryValue<TValue, TOperand>;
            if (queryValue == null) return false;
            return base.Equals(obj)
                && queryValue.Value.Equals(Value)
                && queryValue.Operand.Equals(Operand);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode()
                + Value.GetHashCode()
                + Operand.GetHashCode();
        }
    }
}
