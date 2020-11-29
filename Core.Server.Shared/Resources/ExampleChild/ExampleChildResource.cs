
using Core.Server.Shared.Resources.Infrastructure;

namespace Core.Server.Shared.Resources
{
    public class ExampleChildResource : Resource, IChildResource
    {
        public string ParentId { get; set; }
        public string Name { get; set; }
    }
}
