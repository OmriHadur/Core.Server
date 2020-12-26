using Core.Server.Injection.Interfaces;
using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceCreators.Interfaces;
using Core.Server.Test.ResourceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace Core.Server.Test.DBFilling
{
    [TestClass]
    public class DeleteAllResources : TestsBase
    {
        [Dependency]
        public IReflactionHelper ReflactionHelper;

        [Dependency]
        public IUnityContainer UnityContainer;

        [TestMethod]
        public void TestDeleteAllResources()
        {
            var methodInfo = GetType().GetMethod(nameof(DeleteAllResource));
            var bundels = ReflactionHelper.GetResourcesBoundles();
            foreach (var bundel in bundels)
            {
                var resourceMethod = methodInfo.MakeGenericMethod(bundel.TResource);
                resourceMethod.Invoke(this, new object[0]);
            }
        }

        public void DeleteAllResource<TResource>()
            where TResource : Resource
        {
            var resourceLookup = UnityContainer.Resolve<IResourceLookup<TResource>>();
            var resourceCreate = UnityContainer.Resolve<IResourceCreate<TResource>>();
            var allResources = resourceLookup.Get(false).Value;
            foreach (var resource in allResources)
                resourceCreate.Delete(resource.Id);
        }
    }
}