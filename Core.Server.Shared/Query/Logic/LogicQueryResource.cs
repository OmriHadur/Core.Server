
namespace Core.Server.Shared.Query
{
    public class LogicQueryResource : QueryPropertyResource
    {
        public LogicQueryOperands Operand { get; set; }
        public QueryPropertyResource[] QueryResources { get; set; }
    }
}
