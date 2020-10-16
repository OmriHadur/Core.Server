using Core.Server.Shared.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Test.ResourcesCreators.Interfaces;

namespace Core.Server.Tests.ResourceTests
{
    public abstract class ResourceTestsBase<TResource>
        : TestsBase
        where TResource : Resource
    {
        private readonly IResourcesClean resourcesClean;

        protected TResource CreatedResource;
        protected readonly IResourceQuery<TResource> ResourceQuery;
        protected readonly IResourceCreate<TResource> ResourceCreate;
        
        protected readonly IResourcesIdsHolder ResourcesIdsHolder;

        public ResourceTestsBase()
        {
            ResourceCreate = UnityContainer.Resolve<IResourceCreate<TResource>>();
            ResourcesIdsHolder= UnityContainer.Resolve<IResourcesIdsHolder>();
            ResourceQuery = UnityContainer.Resolve<IResourceQuery<TResource>>();
            ResourceQuery = UnityContainer.Resolve<IResourceQuery<TResource>>();
            resourcesClean = UnityContainer.Resolve<IResourcesClean>();
        }

        [TestInitialize]
        public void TestInitAsync()
        {
            CreateResource();
        }

        [TestCleanup]
        public void Cleanup()
        {
            resourcesClean.Clean();
        }

        protected void CreateResource()
        {
            CreatedResource = ResourceCreate.Create().Value;
        }

        protected int GetAllExistingCount()
        {
            return ResourcesIdsHolder.GetAll<TResource>().Count();
        }

        protected void AssertActionResult<T>(ActionResult<TResource> response)
            where T : ActionResult
        {
            Assert.IsInstanceOfType(response.Result, typeof(T));
        }
    }
}
