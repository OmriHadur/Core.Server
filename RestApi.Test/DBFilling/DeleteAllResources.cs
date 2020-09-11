using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestApi.Client.Interfaces;
using RestApi.Shared.Resources;
using RestApi.Shared.Resources.Users;
using RestApi.Tests.RestRourcesTests;

namespace RestApi.Tests.DBFilling
{
    [TestClass]
    public class DeleteAllResources : TestsBase
    {
        [TestMethod]
        public void TestDeleteAllResources()
        {
            DeleteAllResourcesOfType<LoginCreateResource, LoginResource>();
            DeleteAllResourcesOfType<UserCreateResource, UserResource>();
        }

        private void DeleteAllResourcesOfType<TCreateResource, TResource>()
            where TCreateResource : CreateResource
            where TResource : Resource
        {
            ResourcesHolder.DeleteAll<TResource>();
            var resourceClient = GetClient<IRestClient<TCreateResource, TResource>>();
            var resources = resourceClient.Get().Result;
            foreach (var resource in resources.Value)
                resourceClient.Delete(resource.Id).Wait();
        }
    }
}