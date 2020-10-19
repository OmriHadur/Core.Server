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
    public abstract class ResourceGenericTestsBase<TResource> : TestsBase
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

        public virtual void TestInit()
        { 

        }

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
    }
}
