using Core.Server.Shared.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Core.Server.Client.Results;

namespace Core.Server.Tests.ResourceTests
{
    public abstract class ResourceTestsBase<TCreateResource, TUpdateResource,TResource> :
        TestsBase
        where TCreateResource : CreateResource, new()
        where TUpdateResource: UpdateResource
        where TResource : Resource
    {
        protected TResource CreatedResource;
        protected IResourceCreate<TCreateResource, TUpdateResource,TResource> ResourceCreator;
        public ResourceTestsBase()
        {
            ResourceCreator = TestsUnityContainer.Resolve<IResourceCreate<TCreateResource, TUpdateResource,TResource>>();
        }

        [TestInitialize]
        public void TestInitAsync()
        {
            CreateResource();
        }

        [TestCleanup]
        public void Cleanup()
        {
            ResourcesHolder.DeleteAll();
            ResourcesHolder.DeleteAll();
        }
        protected void CreateResource()
        {
            CreatedResource = ResourcesHolder.Create<TResource>().Value;
        }

        protected int GetAllExistingCount()
        {
            return ResourcesHolder.GetAllExisting<TResource>().Count();
        }

        protected void AssertActionResult<T>(ActionResult<TResource> response)
            where T : ActionResult
        {
            Assert.IsInstanceOfType(response.Result, typeof(T));
        }
    }
}
