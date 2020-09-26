
namespace Core.Server.Shared.Query
{
    public class LogicQueryResource : QueryResource
    {
        public LogicQueryOperands Operand { get; set; }
        public QueryResource[] QueryResources { get; set; }
    }
}
