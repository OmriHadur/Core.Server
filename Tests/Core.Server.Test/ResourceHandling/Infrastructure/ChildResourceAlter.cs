using Core.Server.Client.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceCreation.Interfaces;
using Core.Server.Test.ResourceCreators.Interfaces;
using System;
using System.Linq;
using Unity;

namespace Core.Server.Test.ResourcesCreators.Infrastructure
{
    [Inject]
    public class ChildResourceAlter<TCreateResource, TUpdateResource, TParentResource, TChildResource>
        : ResourceHandling<IChildAlterClient<TCreateResource, TUpdateResource, TParentResource>, TParentResource>
        , IChildResourceAlter<TCreateResource, TUpdateResource, TParentResource, TChildResource>
        where TCreateResource : ChildCreateResource
        where TUpdateResource : ChildUpdateResource
        where TParentResource : Resource
        where TChildResource : Resource
    {
        //[Dependency]
        //public IReflactionHelper ReflactionHelper;

        [Dependency]
        public IRandomResourceCreator<TCreateResource, TUpdateResource, TChildResource> RandomResourceCreator;

        [Dependency]
        public IResourceCreate<TChildResource> ChildResourceCreate;

        [Dependency]
        public IResourceCreate<TParentResource> ParentResourceCreate;

        public ActionResult<TParentResource> Create(Action<TCreateResource> editFunc = null)
        {
            var parent = ParentResourceCreate.GetOrCreate();
            var createResource = RandomResourceCreator.GetRandomCreateResource();
            createResource.ParentId = parent.Id;
            editFunc?.Invoke(createResource);
            return Create(createResource);
        }

        public ActionResult<TParentResource> Create(TCreateResource createResource)
        {
            return Client.Create(createResource).Result;
        }

        public ActionResult<TParentResource> Replace(Action<TCreateResource> editFunc = null)
        {
            var parent = ParentResourceCreate.GetOrCreate();
            var childResource = GetChildResource(parent).Last();
            var replaceResource = RandomResourceCreator.GetRandomCreateResource(childResource);
            replaceResource.ParentId = parent.Id;
            editFunc?.Invoke(replaceResource);
            return Client.Replace(childResource.Id, replaceResource).Result;
        }

        public ActionResult<TParentResource> Update(Action<TUpdateResource> editFunc = null)
        {
            var parent = ParentResourceCreate.GetOrCreate();
            var childResource = GetChildResource(parent).Last();
            var updateResource = RandomResourceCreator.GetRandomUpdateResource(childResource);
            updateResource.ParentId = parent.Id;
            editFunc?.Invoke(updateResource);
            return Client.Update(childResource.Id, updateResource).Result;
        }

        public TChildResource[] GetChildResource(TParentResource parentResource)
        {
            var propertyInfos = parentResource.GetType().GetProperties();
            var propertyInfo = propertyInfos.FirstOrDefault(p => p.PropertyType == typeof(TChildResource[]));
            return (TChildResource[])(propertyInfo.GetValue(parentResource));
        }

        public ActionResult<TParentResource> DeleteLastChild()
        {
            var parent = ParentResourceCreate.GetOrCreate();
            var childResource = GetChildResource(parent).Last();
            var childDeleteResource = new ChildDeleteResource() { ParentId = parent.Id };
            return Client.Delete(childResource.Id, childDeleteResource).Result;
        }
    }
}