using Core.Server.Common.Attributes;
using Core.Server.Test.ResourceCreators.Interfaces;
using Core.Server.Test.ResourcesCreators.Interfaces;
using Unity;

namespace Core.Server.Test.ResourcesCreators.Infrastructure
{
    [Inject]
    public class ResourcesCleaner
        : IResourcesClean
    {
        [Dependency]
        public IUnityContainer UnityContainer;

        [Dependency]
        public IResourcesIdsHolder ResourceIdsHolder;

        public void Clean()
        {
            var types = ResourceIdsHolder.GetAllTypes();
            foreach (var type in types)
            {
                var resourceHandlerType = typeof(IResourceCreate<>);
                var resourceHandlerGenericType = resourceHandlerType.MakeGenericType(type);
                var resourcehandler = (IResourceDelete)UnityContainer.Resolve(resourceHandlerGenericType);
                resourcehandler.DeleteAll();
            }
        }
    }
}
