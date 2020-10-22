using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Unity;
using Core.Server.Injection.Attributes;
using Core.Server.Tests.ResourceTests.Interfaces;
using System;
using Core.Server.Tests.ResourceCreation.Interfaces;
using System.Reflection;

namespace Core.Server.Tests.ResourceTests
{
    [Inject]
    public class ResourceGenericAlterTests<TCreateResource, TUpdateResource, TResource>
        : ResourceGenericTestsBase<TResource>
        , IResourceGenericAlterTests
        where TCreateResource : CreateResource, new()
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {

        [Dependency]
        public IResourceAlter<TCreateResource, TUpdateResource, TResource> resourceAlter;

        [Dependency]
        public IObjectRandomizer ObjectRandomizer;

        [TestMethod]
        public void TestReplace()
        {
            var response = resourceAlter.Replace();
            AssertOk(response);
        }

        [TestMethod]
        public void TestUpdate()
        {
            var randomProperty = GetRandomProperty();
            if (randomProperty == null) return;
            CreateResource();

            var response = resourceAlter.Update(r => ObjectRandomizer.SetRandomValue(r, randomProperty));

            AssertOk(response);
            Assert.AreNotEqual(randomProperty.GetValue(response.Value), randomProperty.GetValue(CreatedResource));
        }

        private PropertyInfo GetRandomProperty()
        {
            var properties = typeof(TUpdateResource).GetProperties();
            if (properties.Length == 0) return null;
            return properties[Random.Next(properties.Length)];
        }
    }
}