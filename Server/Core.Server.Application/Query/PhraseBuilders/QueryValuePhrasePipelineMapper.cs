using Core.Server.Common.Query;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Core.Server.Application.Query.PhraseBuilders
{
    public abstract class QueryValuePhrasePipelineMapper<TQueryValue, TValue, TOperand>
        : QueryPhrasePipelineMapper
        where TQueryValue : QueryValue<TValue, TOperand>, new()
    {
        protected const string FieldGroupName = "field";
        protected const string OperandGroupName = "operand";
        protected const string ValueGroupName = "value";

        protected override QueryBase InnerMap(string queryPhrase, IEnumerable<IQueryPhrasePipelineMapper> mappers)
        {
            var match = Regex.Match(queryPhrase, MappingRegex);
            var field = ToStartUpper(match.Groups[FieldGroupName].Value);
            var operandString = ToStartUpper(match.Groups[OperandGroupName].Value);
            var operand = GetOperand(operandString);
            var value = GetValue(match.Groups[ValueGroupName].Value);
            return new TQueryValue() { Field = field, Operand = operand, Value = value };
        }

        protected virtual TOperand GetOperand(string value)
        {
            return (TOperand)Enum.Parse(typeof(TOperand), value);
        }

        protected abstract TValue GetValue(string value);
    }
}
