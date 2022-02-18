using Core.Server.Common.Attributes;
using Core.Server.Common.Query;

namespace Core.Server.Application.Query.PhraseBuilders
{
    [InjectName]
    public class StringQueryPhrasePipelineMapper
        : QueryValuePhrasePipelineMapper<QueryString, string, QueryStringOperands>
    {
        public override int Priory => 2;

        protected override string MappingRegex =>
            @$"(?'{FieldGroupName}'\w*)\.(?'{OperandGroupName}'\w*)\((?'{ValueGroupName}'[^)]+)\)";

        protected override string GetValue(string value)
        {
            return value;
        }
    }
}
