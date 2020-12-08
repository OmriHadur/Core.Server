
namespace Core.Server.Shared.Resources
{
    public class ExampleResource : Resource
    {
        public int Value { get; set; }

        public string Name { get; set; }

        public string Mutable { get; set; }

        public ExampleChildResource[] ChildResources { get; set; }

        public ExampleResource()
        {
            ChildResources = new ExampleChildResource[0];
        }
    }
}
