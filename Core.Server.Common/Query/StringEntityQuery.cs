
namespace Core.Server.Common.Query
{
    public class StringEntityQuery : QueryEntityBase
    {
        public string Field { get; set; }
        public string Regex { get; set; }
    }
}
