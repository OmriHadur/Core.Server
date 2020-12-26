using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources
{
    public class RoleUpdateResource : UpdateResource
    {
        public string Name { get; set; }

        public string[] PoliciesId;
    }
}
