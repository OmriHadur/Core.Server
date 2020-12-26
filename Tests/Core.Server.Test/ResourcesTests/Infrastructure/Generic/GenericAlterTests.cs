using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceCreators.Interfaces;
using Core.Server.Test.ResourceTests.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity;

namespace Core.Server.Test.ResourceTests
{
    [Inject]
    public class GenericAlterTests<TCreateResource, TUpdateResource, TResource>
        : GenericTestsBase<TResource>
        , IResourceGenericAlterTests
        where TCreateResource : CreateResource
        where TUpdateResource : UpdateResource
        where TResource : Resource
    {
        [Dependency]
        public IResourceAlter<TCreateResource, TUpdateResource, TResource> resourceAlter;

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
            var getResponse = ResourceLookup.Get(replaceResponse.Value.Id);
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
            var getResponse = ResourceLookup.Get(updateResponse.Value.Id);
            Validate(updateResponse, getResponse);
        }
    }
}