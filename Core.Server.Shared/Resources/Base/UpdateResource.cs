
using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources
{
    public class UpdateResource
    {
        [Required]
        public string Id { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is UpdateResource)) return false;
            var id = (obj as UpdateResource).Id;
            if (id == null) return false;
            return id.Equals(Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
