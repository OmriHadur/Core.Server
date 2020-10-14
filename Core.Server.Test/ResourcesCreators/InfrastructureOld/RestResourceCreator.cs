using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Unity;

namespace Core.Server.Tests.ResourceCreators
{
    public abstract class RestResourceCreator<TCreateResource, TUpdateResource, TResource> :
        ResourceCreatorBase,
        IResourceCreator<TCreateResource, TUpdateResource,TResource>
        where TCreateResource : CreateResource, new()
        where TUpdateResource : UpdateResource, new()
        where TResource : Resource
    {
        [Dependency]
        public IRestClient<TCreateResource, TUpdateResource ,TResource> _restClient;

        public ActionResult<TResource> Create()
        {
            var resource = GetRandomCreateResource();
            return RestClient.Create(resource).Result;
        }

        public ActionResult<TResource> Create(TCreateResource createResource)
        {
            return RestClient.Create(createResource).Result;
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

        public TUpdateResource GetRandomUpdateResource()
        {
            var updateResource = new TUpdateResource();
            SetUpdatResource(updateResource);
            return updateResource;
        }

        public virtual void SetCreateResource(TCreateResource createResource)
        {
            ObjectRandomizer.AddRandomValues(createResource);
        }

        public virtual void SetUpdatResource(TUpdateResource updateResource)
        {
            ObjectRandomizer.AddRandomValues(updateResource);
        }

        public ActionResult<TResource> Update(string id, TUpdateResource updateResource)
        {
            return RestClient.Update(id, updateResource).Result;
        }

        protected IRestClient<TCreateResource, TUpdateResource,TResource> RestClient
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