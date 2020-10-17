using Core.Server.Shared.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Core.Server.Client.Results;
using Core.Server.Test.ResourcesCreators.Interfaces;
using Unity;
using System;

namespace Core.Server.Tests.ResourceTests
{
    public abstract class ResourceTestsBase<TResource>
        : TestsBase
        where TResource : Resource
    {
        protected TResource CreatedResource;

        [Dependency]
        public IResourcesClean ResourcesClean;

        [Dependency]
        public IResourceQuery<TResource> ResourceQuery;

        [Dependency]
        public IResourceCreate<TResource> ResourceCreate;

        [Dependency]
        public IResourcesIdsHolder ResourcesIdsHolder;

        [TestInitialize]
        public void TestInit()
        {
            CreateResource();
        }

        [TestCleanup]
        public void Cleanup()
        {
            ResourcesClean.Clean();
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
