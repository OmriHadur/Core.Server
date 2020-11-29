using Core.Server.Shared.Resources.Infrastructure;

namespace Core.Server.Shared.Resources
{
    public class ExampleChildUpdateResource : UpdateResource, IChildResource
    {
        public string ParentId { get; set; }

        public string Name { get; set; }
    }
}
