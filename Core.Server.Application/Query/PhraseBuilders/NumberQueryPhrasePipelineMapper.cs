using Core.Server.Common.Query;
using Core.Server.Injection.Attributes;
using System;
using System.Text;

namespace Core.Server.Application.Query.PhraseBuilders
{
    [Inject]
    public class NumberQueryPhrasePipelineMapper
        : QueryValuePhrasePipelineMapper<QueryNumber, double, QueryNumberOperands>
    {
        public override int Priory => 2;

        protected override string MappingRegex
        {
            get
            {
                var sb = new StringBuilder();
                sb.Append("(?'");
                sb.Append(FieldGroupName);
                sb.Append("'\\w*) (?'");
                sb.Append(OperandGroupName);
                sb.Append("'[<>=]+) (?'");
                sb.Append(ValueGroupName);
                sb.Append(@"'\d\.{0,1}\d*)+");
                return sb.ToString();
            }
        }
        protected override QueryNumberOperands GetOperand(string value)
        {
            switch (value)
            {
                case "<": return QueryNumberOperands.LessThen;
                case "<=": return QueryNumberOperands.LessThenOrEquals;
                case "==": return QueryNumberOperands.Equals;
                case ">": return QueryNumberOperands.GreaterThen;
                case ">=": return QueryNumberOperands.GreaterThenOrEquals;
            }
            throw new Exception();
        }
        protected override double GetValue(string value)
        {
            return double.Parse(value);
        }
    }
}
