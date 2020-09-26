using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core.Server.Client.Interfaces;
using Core.Server.Shared.Resources;
using Core.Server.Shared.Resources.Users;
using Core.Server.Tests.ResourceTests;

namespace Core.Server.Tests.DBFilling
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