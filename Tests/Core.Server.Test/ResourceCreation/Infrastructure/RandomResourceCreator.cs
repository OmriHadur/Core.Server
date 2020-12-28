using Core.Server.Common.Attributes;
using Core.Server.Injection.Interfaces;
using Core.Server.Shared.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceCreation.Interfaces;
using System;
using System.Reflection;
using Unity;

namespace Core.Server.Test.ResourceCreation
{
    [Inject]
    public class RandomResourceCreator<TAlterResource, TResource>
        : IRandomResourceCreator<TAlterResource, TResource>
        where TAlterResource : new()
        where TResource: Resource
    {
        [Dependency]
        public IObjectRandomizer ObjectRandomizer;

        [Dependency]
        public IReflactionHelper ReflactionHelper;

        public TAlterResource GetRandomCreateResource()
        {
            var createResource = new TAlterResource();
            AddRandomValues(createResource);
            return createResource;
        }

        public TAlterResource GetRandomReplacResource(TResource existingResource)
        {
            var createResource = new TAlterResource();
            AddRandomToExistingValues(createResource, existingResource);
            return createResource;
        }

        public TAlterResource GetRandomUpdateResource(TResource existingResource)
        {
            var createResource = new TAlterResource();
            AddRandomToExistingValues(createResource, existingResource);
            return createResource;
        }

        protected virtual void AddRandomValues(TAlterResource createResource)
        {
            ObjectRandomizer.AddRandomValues(createResource);
        }


        protected virtual void AddRandomToExistingValues(TAlterResource alterResource, TResource existingResource)
        {
            ObjectRandomizer.AddRandomValues(alterResource);
            var properties = ReflactionHelper.GetPropertiesWithAttribute<ImmutableAttribute>(alterResource);
            foreach (var property in properties)
                property.SetValue(alterResource, null);
        }

        protected virtual void AddRandomUpdateValues(TAlterResource updateResource)
        {
            var randomProperty = ObjectRandomizer.GetRandomProperty<TAlterResource>();
            if (randomProperty != null)
                ObjectRandomizer.SetRandomValue(updateResource, randomProperty);
        }
    }
}