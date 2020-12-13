using System.ComponentModel.DataAnnotations;

namespace Core.Server.Shared.Resources
{
    public class PolicyUpdateResource : UpdateResource
    {
        public string Name { get; set; }

        public string ResourceType { get; set; }

        public ResourceActions ResourceActions { get; set; }
    }
}
