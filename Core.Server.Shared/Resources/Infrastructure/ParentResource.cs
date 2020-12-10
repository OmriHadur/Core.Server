namespace Core.Server.Shared.Resources
{
    public abstract class ParentResource : Resource
    {
        public abstract TChildResource[] GetChildResources<TChildResource>()
            where TChildResource : ChildResource;
    }
}
