using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using System.Collections.Generic;
using Unity;

namespace Core.Server.Tests.ResourceTests
{
    public abstract class GenericTestsBase<TResource> 
        : TestsBase
        where TResource : Resource
    {
        protected TResource CreatedResource;

        [Dependency]
        public IResourceLookup<TResource> ResourceLookup;

        [Dependency]
        public IResourceCreate<TResource> ResourceCreate;

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
    }
}
