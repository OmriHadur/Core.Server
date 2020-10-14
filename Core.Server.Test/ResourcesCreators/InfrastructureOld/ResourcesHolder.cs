using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Client.Results;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Core.Server.Tests.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;
using Core.Server.Shared.Resources.Users;

namespace Core.Server.Tests.ResourceCreators
{
    public class ResourcesHolder : IResourcesIdHolder
    {
        private IDictionary<Type, IList<string>> _resourcesIdsPerType;

        [Dependency]
        public IUnityContainer unityContainer;

        public ResourcesHolder()
        {
            _resourcesIdsPerType = new Dictionary<Type, IList<string>>();
        }

        public IEnumerable<TResource> GetAllExisting<TResource>()
            where TResource : Resource
        {
            if (HasType<TResource>())
            {
                var ids = _resourcesIdsPerType[typeof(TResource)];
                foreach (var id in ids)
                    yield return GetGetter<TResource>().Get(id).Value;
            }
        }

        public ActionResult<TResource> GetLastOrCreate<TResource>()
            where TResource : Resource
        {
            if (HasId<TResource>())
                return GetGetter<TResource>().Get(_resourcesIdsPerType[typeof(TResource)].Last());
            else
                return Create<TResource>();
        }

        public ActionResult<TResource> Create<TResource>()
            where TResource : Resource
        {
            var resourceResult = GetGetter<TResource>().Create();
            AddId(resourceResult.Value);
            return resourceResult;
        }

        public ActionResult<TResource> Create<TCreateResource, TUpdateResource,TResource>(TCreateResource createResource)
            where TCreateResource : CreateResource, new()
            where TUpdateResource : UpdateResource
            where TResource : Resource
        {
            var resourceResult = GetCreator<TCreateResource, TUpdateResource, TResource >().Create(createResource);
            AddId(resourceResult.Value);
            return resourceResult;
        }

        public ActionResult<TResource> EditAndCreate<TCreateResource, TUpdateResource,TResource>(Action<TCreateResource> edit)
            where TCreateResource : CreateResource, new()
            where TUpdateResource : UpdateResource
            where TResource : Resource
        {
            var resourceCreator = GetCreator<TCreateResource, TUpdateResource, TResource >();
            var createResource = resourceCreator.GetRandomCreateResource();
            edit(createResource);
            return Create<TCreateResource, TUpdateResource,TResource>(createResource);
        }

        public void DeleteAll<TResource>()
            where TResource : Resource
        {
            Delete(typeof(TResource));
        }

        public void DeleteAll()
        {
            unityContainer.Resolve<ITokenHandler>().Login();
            foreach (var resourcesType in _resourcesIdsPerType.Keys)
                Delete(resourcesType);
        }

        public UserResource GetLoggedUser()
        {
            unityContainer.Resolve<ITokenHandler>().Login();
            return GetLastOrCreate<UserResource>().Value;
        }

        public ActionResult Delete<TResource>(string resourceId)
                        where TResource : Resource
        {
            var result = GetDeleter(typeof(TResource)).Delete(resourceId);
            if (result == null || result is OkResult)
                _resourcesIdsPerType[typeof(TResource)].Remove(resourceId);
            return result;
        }

        public ActionResult<TResource> Get<TResource>(string id)
            where TResource : Resource
        {
            return GetGetter<TResource>().Get(id);
        }

        public string GetResourceId<TResource>() where TResource : Resource
        {
            if (HasType<TResource>())
                return GetResourceIds<TResource>().Last();
            else
            {
                var result = Create<TResource>();
                if (result.Result != null)
                    return result.Value.Id; ;
                return result.Value.Id;
            }
        }

        public IEnumerable<string> GetResourceIds<TResource>() where TResource : Resource
        {
            if (!HasType<TResource>())
                return new string[0];
            return _resourcesIdsPerType[typeof(TResource)];
        }

        private bool HasId<TResource>() where TResource : Resource
        {
            return GetResourceId<TResource>() != null;
        }

        private void Delete(Type type)
        {
            if (!_resourcesIdsPerType.ContainsKey(type))
                return;
            if (_resourcesIdsPerType[type].Count == 0)
            {
                _resourcesIdsPerType.Remove(type);
                return;
            }
            var resourceDeleter = GetDeleter(type);
            foreach (var resourceId in _resourcesIdsPerType[type].Reverse())
            {
                if (!IsInstanceOfGenericType(typeof(InnerRestResourceCreator<,,,>), resourceDeleter))
                    resourceDeleter.Delete(resourceId);
            }
            _resourcesIdsPerType[type].Clear();
        }

        private static bool IsInstanceOfGenericType(Type genericType, object instance)
        {
            Type type = instance.GetType();
            while (type != null)
            {
                if (type.IsGenericType &&
                    type.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
                type = type.BaseType;
            }
            return false;
        }


        private IResourceCreator<TCreateResource, TUpdateResource ,TResource> GetCreator<TCreateResource, TUpdateResource,TResource>()
            where TCreateResource : CreateResource, new()
            where TUpdateResource: UpdateResource
            where TResource : Resource
        {
            return unityContainer.Resolve<IResourceCreator<TCreateResource, TUpdateResource,TResource>>();
        }

        private IResourcerHandler<TResource> GetGetter<TResource>()
                where TResource : Resource
        {
            return unityContainer.Resolve<IResourcerHandler<TResource>>();
        }

        private IResourceDeleter GetDeleter(Type type)
        {
            return unityContainer.Resolve<IResourceDeleter>(type.Name);
        }

        private bool HasType<TResource>() where TResource : Resource
        {
            return _resourcesIdsPerType.ContainsKey(typeof(TResource)) &&
                _resourcesIdsPerType[typeof(TResource)].Count > 0;
        }

        private void AddId<TResource>(TResource resource)
            where TResource : Resource
        {
            if (resource != null)
            {
                var type = typeof(TResource);
                if (_resourcesIdsPerType.ContainsKey(type))
                    _resourcesIdsPerType[type].Add(resource.Id);
                else
                {
                    var ids = new List<string>() { resource.Id };
                    _resourcesIdsPerType.Add(type, ids);
                }
            }
        }
    }
}
