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
    public class ChildResourceAlter<TChildAlterResource, TParentResource, TChildResource>
        : ResourceHandling<IChildAlterClient<TChildAlterResource, TParentResource>, TParentResource>
        , IChildResourceAlter<TChildAlterResource, TParentResource, TChildResource>
        where TChildAlterResource : ChildAlterResource
        where TParentResource : Resource
        where TChildResource : Resource
    {
        //[Dependency]
        //public IReflactionHelper ReflactionHelper;

        [Dependency]
        public IRandomResourceCreator<TChildAlterResource, TChildResource> RandomResourceCreator;

        [Dependency]
        public IResourceCreate<TChildResource> ChildResourceCreate;

        [Dependency]
        public IResourceCreate<TParentResource> ParentResourceCreate;

        public ActionResult<TParentResource> Create(Action<TChildAlterResource> editFunc = null)
        {
            var parent = ParentResourceCreate.GetOrCreate();
            var createResource = RandomResourceCreator.GetRandomCreateResource();
            createResource.ParentId = parent.Id;
            editFunc?.Invoke(createResource);
            return Create(createResource);
        }

        public ActionResult<TParentResource> Create(TChildAlterResource createResource)
        {
            return Client.Create(createResource).Result;
        }

        public ActionResult<TParentResource> Replace(Action<TChildAlterResource> editFunc = null)
        {
            var parent = ParentResourceCreate.GetOrCreate();
            var childResource = GetChildResource(parent).Last();
            var replaceResource = RandomResourceCreator.GetRandomUpdateResource(childResource);
            replaceResource.ParentId = parent.Id;
            editFunc?.Invoke(replaceResource);
            return Client.Replace(childResource.Id, replaceResource).Result;
        }

        public ActionResult<TParentResource> Update(Action<TChildAlterResource> editFunc = null)
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
            var childDeleteResource = new ChildAlterResource() { ParentId = parent.Id };
            return Client.Delete(childResource.Id, childDeleteResource).Result;
        }
    }
}