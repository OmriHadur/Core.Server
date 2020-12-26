using Core.Server.Injection.Interfaces;
using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceCreators.Interfaces;
using Core.Server.Test.ResourceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace Core.Server.Test.DBFilling
{
    [TestClass]
    public class FillDatabase : TestsBase
    {
        protected int SCALE = 50;

        [Dependency]
        public IReflactionHelper ReflactionHelper;

        [Dependency]
        public IUnityContainer UnityContainer;

        [TestMethod]
        public void TestDeleteAllResources()
        {
            var methodInfo = GetType().GetMethod(nameof(CreateResource));
            var bundels = ReflactionHelper.GetResourcesBoundles();
            foreach (var bundel in bundels)
            {
                var resourceMethod = methodInfo.MakeGenericMethod(bundel.TResource);
                resourceMethod.Invoke(this, new object[0]);
            }
        }

        public void CreateResource<TResource>()
            where TResource : Resource
        {
            var resourceCreate = UnityContainer.Resolve<IResourceCreate<TResource>>();
            for (int i = 0; i < SCALE; i++)
                resourceCreate.Create();
        }
    }
}