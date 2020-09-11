using RestApi.Client.Interfaces;
using RestApi.Client.Results;
using RestApi.Shared.Resources;
using RestApi.Tests.RestResourcesCreators.Interfaces;
using Unity;

namespace RestApi.Tests.RestResourcesCreators
{
    public abstract class RestResourceCreator<TCreateResource, TResource> :
        ResourceCreatorBase,
        IResourceCreator<TCreateResource, TResource>
        where TCreateResource : CreateResource, new()
        where TResource : Resource
    {
        [Dependency]
        public IRestClient<TCreateResource, TResource> _restClient;

        public ActionResult<TResource> Create()
        {
            var resource = GetRandomCreateResource();
            return RestClient.Create(resource).Result;
        }

        public ActionResult<TResource> Create(TCreateResource createResource)
        {
            return RestClient.Create(createResource as TCreateResource).Result;
        }

        public ActionResult Delete(string id)
        {
            return RestClient.Delete(id).Result.Result;
        }

        public ActionResult<TResource> Get(string id)
        {
            return RestClient.Get(id).Result;
        }

        public TCreateResource GetRandomCreateResource()
        {
            var createdResource = new TCreateResource();
            SetCreateResource(createdResource);
            return createdResource;
        }
        public virtual void SetCreateResource(TCreateResource createResource)
        {
            ObjectRandomizer.AddRandomValues(createResource);
        }

        public ActionResult<TResource> Update(string id, TCreateResource resourceToCreate)
        {
            return RestClient.Update(id, resourceToCreate as TCreateResource).Result;
        }

        protected IRestClient<TCreateResource, TResource> RestClient
        {
            get
            {
                if (_restClient.ServerUrl == null)
                    _restClient.ServerUrl = ConfigHandler.Config.ServerUrl;
                if (_restClient.Token == null)
                {
                    TokenHandler.OnTokenChange += (s, t) => _restClient.Token = t;
                    _restClient.Token = TokenHandler.Token;              
                }

                return _restClient;
            }
        }
    }
}