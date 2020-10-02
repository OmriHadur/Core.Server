using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Unity;

namespace Core.Server.Tests.ResourceCreators
{
    public abstract class InnerRestResourceCreator<TCreateResource, TUpdateResource,TResource, TParentResource> :
        ResourceCreatorBase,
        IResourceCreator<TCreateResource, TUpdateResource,TResource>
        where TCreateResource : CreateResource, new()
        where TUpdateResource : UpdateResource, new()
        where TResource : Resource
        where TParentResource : Resource
    {
        [Dependency]
        public IInnerRestClient<TCreateResource, TUpdateResource,TResource> _restInnerClient;

        public ActionResult<TResource> Create()
        {
            var resource = GetRandomCreateResource();
            return RestInnerClient.Create(ParentId, resource).Result;
        }

        public ActionResult<TResource> Create(TCreateResource createResource)
        {
            return RestInnerClient.Create(ParentId, createResource as TCreateResource).Result;
        }

        public ActionResult Delete(string id)
        {
            return RestInnerClient.Delete(ParentId, id).Result.Result;
        }

        public ActionResult<TResource> Get(string id)
        {
            return RestInnerClient.Get(ParentId, id).Result;
        }

        public virtual TCreateResource GetRandomCreateResource()
        {
            var createdResource = new TCreateResource();
            ObjectRandomizer.AddRandomValues(createdResource);
            return createdResource;
        }

        public ActionResult<TResource> Update(string id, TUpdateResource updateResource)
        {
            return RestInnerClient.Update(ParentId, id, updateResource).Result;
        }

        public TUpdateResource GetRandomUpdateResource()
        {
            var updateResource = new TUpdateResource();
            ObjectRandomizer.AddRandomValues(updateResource);
            return updateResource;
        }

        protected string ParentId => GetResourceId<TParentResource>();

        protected IInnerRestClient<TCreateResource, TUpdateResource, TResource> RestInnerClient
        {
            get
            {
                if (_restInnerClient.ServerUrl == null)
                    _restInnerClient.ServerUrl = ConfigHandler.Config.ServerUrl;
                if (_restInnerClient.Token == null)
                {
                    TokenHandler.OnTokenChange += (s, t) => _restInnerClient.Token = t;
                    _restInnerClient.Token = TokenHandler.Token;
                }
                return _restInnerClient;
            }
        }
    }
}
