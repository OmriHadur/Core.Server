using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceCreation.Interfaces;
using Core.Server.Test.ResourceCreators.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace Core.Server.Test.ResourcesCreators.Infrastructure
{
    [Inject]
    public class ResourceBatach<TAlterResource, TResource>
        : ResourceHandling<IBatchClient<TAlterResource, TResource>, TResource>
        , IResourceBatch<TAlterResource, TResource>
        where TResource : Resource
    {
        [Dependency]
        public IRandomResourceCreator<TAlterResource, TResource> RandomResourceCreator;

        public ActionResult<IEnumerable<TResource>> Create(int amount)
        {
            var createResources = new TAlterResource[amount];
            for (int i = 0; i < amount; i++)
                createResources[i] = RandomResourceCreator.GetRandomCreateResource();
            return Create(createResources);
        }

        public ActionResult<IEnumerable<TResource>> Create(IEnumerable<TAlterResource> createResources)
        {
            var result = Client.BatchCreate(createResources.ToArray()).Result;
            if (result.IsSuccess)
                foreach (var resource in result.Value)
                    ResourceIdsHolder.Add<TResource>(resource.Id);
            return result;
        }

        public ActionResult<IEnumerable<string>> Delete(IEnumerable<string> ids)
        {
            var result = Client.BatchDelete(ids.ToArray()).Result;
            if (result.IsSuccess)
                foreach (var id in result.Value)
                    ResourceIdsHolder.Remove<TResource>(id);
            return result;
        }

        public ActionResult<IEnumerable<TResource>> Update(IEnumerable<TAlterResource> updateResources)
        {
            return Client.BatchUpdate(updateResources.ToArray()).Result;
        }
    }
}