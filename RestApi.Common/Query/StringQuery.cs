
namespace RestApi.Common.Query
{
    public class StringQuery : QueryBase
    {
        public string Field { get; set; }
        public string Regex { get; set; }
    }
}
