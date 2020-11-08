using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Unity;
using Core.Server.Common.Attributes;
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
        public void TestReplaceCreated()
        {
            var replaceResponse = resourceAlter.Replace();
            var getResponse = ResourceQuery.Get(replaceResponse.Value.Id);
            AssertOk(getResponse);
            Validate(replaceResponse.Value, getResponse.Value);
        }

        [TestMethod]
        public void TestUpdate()
        {
            var response = resourceAlter.Update();

            AssertOk(response);
        }

        [TestMethod]
        public void TestGetAfterUpdate()
        {
            var updateResponse = resourceAlter.Update();
            var getResponse = ResourceQuery.Get(updateResponse.Value.Id);
            Validate(updateResponse, getResponse);
        }
    }
}