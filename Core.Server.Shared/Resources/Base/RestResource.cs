
namespace Core.Server.Shared.Resources.Base
{
    public class RestResource<TCreateResource, TUpdateResource, TResource>
        where TCreateResource : CreateResource, new()
        where TUpdateResource : UpdateResource, new()
        where TResource : Resource, new()
    {
    }
}