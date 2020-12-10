namespace Core.Server.Shared.Resources
{
    public interface IParentResource
    {
        TChildResource[] GetChildResources<TChildResource>()
            where TChildResource : ChildResource;
    }
}
