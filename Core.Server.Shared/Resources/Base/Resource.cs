
namespace Core.Server.Shared.Resources
{
    public class Resource
    {
        public string Id { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Resource)) return false;
            var id = (obj as Resource).Id;
            if (id == null) return false;
            return id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
