using Core.Server.Test.ResourcesCreators.Interfaces;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Unity;

namespace Core.Server.Test.ResourcesCreators.Infrastructure
{
    public class ResourcesCleaner
        : IResourcesCleaner
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
                var resourceHandlerType = typeof(IAlterResource<>);
                var resourceHandlerGenericType= resourceHandlerType.MakeGenericType(type);
                var resourcehandler = UnityContainer.Resolve(resourceHandlerGenericType);
                (resourcehandler as IResourceDeleter).DeleteAll();
            }
        }
    }
}
