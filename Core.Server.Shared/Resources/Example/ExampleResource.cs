
namespace Core.Server.Shared.Resources
{
    //[ResourceBoudle(nameof(ExampleResource),"TResource")]
    public class ExampleResource : Resource, IParentResource
    {
        public int Value { get; set; }

        public string Name { get; set; }

        public string Mutable { get; set; }

        public ExampleChildResource[] ChildResources { get; set; }

        public ExampleResource()
        {
            ChildResources = new ExampleChildResource[0];
        }

        public TChildResource[] GetChildResources<TChildResource>() 
            where TChildResource : ChildResource
        {
            if (typeof(TChildResource) == typeof(ExampleChildResource))
                return (ChildResources as TChildResource[]);
            return null;
        }
    }
}