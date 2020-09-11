using RestApi.Client.Interfaces;
using RestApi.Client.Results;
using RestApi.Shared.Resources;
using RestApi.Tests.RestResourcesCreators.Interfaces;
using Unity;

namespace RestApi.Tests.RestResourcesCreators
{
    public abstract class InnerRestResourceCreator<TCreateResource, TResource, TParentResource> :
        ResourceCreatorBase,
        IResourceCreator<TCreateResource, TResource>
        where TCreateResource : CreateResource, new()
        where TResource : Resource
        where TParentResource : Resource
    {
        [Dependency]
        public IInnerRestClient<TCreateResource, TResource> _restInnerClient;

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

        public ActionResult<TResource> Update(string id, TCreateResource resourceToCreate)
        {
            return RestInnerClient.Update(ParentId, id, resourceToCreate as TCreateResource).Result;
        }

        protected string ParentId => GetResourceId<TParentResource>();

        protected IInnerRestClient<TCreateResource, TResource> RestInnerClient
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
