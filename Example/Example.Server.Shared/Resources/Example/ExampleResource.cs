using Core.Server.Shared.Resources;

namespace Example.Server.Shared.Resources
{
    //[ResourceBoudle(nameof(ExampleResource),"TResource")]
    public class ExampleResource : Resource
    {
        public int Value { get; set; }

        public string Name { get; set; }

        public ExampleChildResource[] ChildResources { get; set; }
    }
}