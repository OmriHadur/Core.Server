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
            DeleteAllResourcesOfType<LoginCreateResource,LoginUpdateResource, LoginResource>();
            DeleteAllResourcesOfType<UserCreateResource,UserUpdateResource, UserResource>();
        }

        private void DeleteAllResourcesOfType<TCreateResource, TUpdateResource, TResource>()
            where TCreateResource : CreateResource
            where TUpdateResource: UpdateResource
            where TResource : Resource
        {
            ResourcesHolder.DeleteAll<TResource>();
            var resourceClient = GetClient<IRestClient<TCreateResource, TUpdateResource, TResource >>();
            var resources = resourceClient.Get().Result;
            foreach (var resource in resources.Value)
                resourceClient.Delete(resource.Id).Wait();
        }
    }
}