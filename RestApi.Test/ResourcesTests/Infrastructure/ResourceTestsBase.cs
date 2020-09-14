using RestApi.Shared.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using RestApi.Tests.ResourceCreators.Interfaces;
using RestApi.Client.Results;

namespace RestApi.Tests.ResourceTests
{
    public abstract class ResourceTestsBase<TCreateResource,TResource> :
        TestsBase
        where TCreateResource : CreateResource, new()
        where TResource : Resource
    {
        protected TResource CreatedResource;
        protected IResourceCreator<TCreateResource,TResource> ResourceCreator;
        public ResourceTestsBase()
        {
            ResourceCreator = TestsUnityContainer.Resolve<IResourceCreator<TCreateResource, TResource>>();
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
