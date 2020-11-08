using Core.Server.Common.Query;
using Core.Server.Common.Attributes;

namespace Core.Server.Application.Query.PhraseBuilders
{
    [InjectName]
    public class StringQueryPhrasePipelineMapper 
        : QueryValuePhrasePipelineMapper<QueryString, string, QueryStringOperands>
    {
        public override int Priory => 2;

        protected override string MappingRegex => 
            @$"(?'{FieldGroupName}'\w*)\.(?'{OperandGroupName}'\w*)\((?'{ValueGroupName}'\w*)\)";

        protected override string GetValue(string value)
        {
            return value;
        }
    }
}
