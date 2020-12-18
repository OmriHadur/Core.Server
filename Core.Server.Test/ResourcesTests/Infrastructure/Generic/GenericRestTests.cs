using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceTests.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Core.Server.Test.ResourceTests
{
    public class GenericRestTests<TResource>
        : GenericTestsBase<TResource>
        , IResourceGenericRestTests
        where TResource : Resource
    {
        public override void TestInit()
        {
            CreateResource();
        }

        public virtual void TestCreateAddedToList()
        {
            var idsCount = ResourcesIdsHolder.GetAll<TResource>().Count();
            var listCount = ResourceLookup.Get().Value.Count();
            Assert.AreEqual(idsCount, listCount);
        }

        public virtual void TestGet()
        {
            var id = ResourcesIdsHolder.GetLast<TResource>();
            var result = ResourceLookup.Get(id);
            AssertNoError(result);
            Validate(CreatedResource, result.Value);
        }


        public virtual void TestGetNotFound()
        {
            var response = ResourceLookup.Get("5fb00552d72114101e33fa47");
            AssertNotFound(response);
        }


        public virtual void TestDelete()
        {
            var response = ResourceCreate.Delete(CreatedResource.Id);
            AssertOk(response);
        }


        public virtual void TestGetNotFoundAfterDelete()
        {
            ResourceCreate.Delete(CreatedResource.Id);
            var response = ResourceLookup.Get(CreatedResource.Id);
            AssertNotFound(response);
        }


        public virtual void TestDeleteNotFoundAfterDelete()
        {
            ResourceCreate.Delete(CreatedResource.Id);
            var response = ResourceCreate.Delete(CreatedResource.Id);
            AssertNotFound(response);
        }
    }
}