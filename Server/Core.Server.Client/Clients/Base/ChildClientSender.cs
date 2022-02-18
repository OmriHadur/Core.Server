using Core.Server.Shared.Resources;

namespace Core.Server.Client.Clients
{
    public class ChildClientSender<TParentResource, TChildResource> :
        ClientSender<TParentResource>
        where TParentResource : Resource
        where TChildResource : Resource
    {
        protected override string GetApiRoute()
        {
            var parentName = typeof(TParentResource).Name.Replace(nameof(Resource), string.Empty);
            var childName = typeof(TChildResource).Name
                .Replace(nameof(Resource), string.Empty)
                .Replace(parentName, string.Empty);

            return parentName + "/" + childName;
        }
    }
}
