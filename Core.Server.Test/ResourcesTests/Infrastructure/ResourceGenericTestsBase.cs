using Core.Server.Shared.Resources;
using System.Linq;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Core.Server.Test.ResourcesCreators.Interfaces;
using Unity;
using Core.Server.Tests.Utils;

namespace Core.Server.Tests.ResourceTests
{
    public abstract class ResourceGenericTestsBase<TResource> : TestsBase
        where TResource : Resource
    {
        protected TResource CreatedResource;

        [Dependency]
        public IResourcesClean ResourcesClean;

        [Dependency]
        public IResourceLookup<TResource> ResourceQuery;

        [Dependency]
        public IResourceCreate<TResource> ResourceCreate;

        [Dependency]
        public IResourcesIdsHolder ResourcesIdsHolder;

        [Dependency]
        public ICurrentUser CurrentUser;

        public virtual void TestInit()
        { 

        }

        public void Cleanup()
        {
            ResourcesClean.Clean();
            CurrentUser.Logout();
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
