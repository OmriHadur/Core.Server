
namespace Core.Server.Common.Query
{
    public class QueryField : QueryBase
    {
        public string Field { get; set; }

        public override bool Equals(object obj)
        {
            return (obj as QueryField)?.Field == Field;
        }
        public override int GetHashCode()
        {
            return Field.GetHashCode();
        }
    }
}
