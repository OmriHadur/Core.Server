using Core.Server.Shared.Resources;
using Core.Server.Test.ResourcesCreators.Interfaces;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Core.Server.Tests.Utils;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace Core.Server.Tests.ResourceTests
{
    public abstract class GenericTestsBase<TResource>  : TestsBase
        where TResource : Resource
    {
        protected TResource CreatedResource;

        [Dependency]
        public IResourcesClean ResourcesClean;

        [Dependency]
        public IResourceLookup<TResource> ResourceLookup;

        [Dependency]
        public IResourceCreate<TResource> ResourceCreate;

        [Dependency]
        public IResourcesIdsHolder ResourcesIdsHolder;

        [Dependency]
        public ICurrentUser CurrentUser;

        public virtual void TestInit()
        { 

        }

        public virtual void Cleanup()
        {
            ResourcesClean.Clean();
            CurrentUser.Logout();
        }

        protected TResource CreateResource()
        {
            CreatedResource = ResourceCreate.Create().Value;
            return CreatedResource;
        }

        protected IEnumerable<TResource> CreateResources(int amount)
        {
            for (int i = 0; i < amount; i++)
                yield return CreateResource();
        }

        protected int GetAllExistingCount()
        {
            return ResourcesIdsHolder.GetAll<TResource>().Count();
        }
    }
}
