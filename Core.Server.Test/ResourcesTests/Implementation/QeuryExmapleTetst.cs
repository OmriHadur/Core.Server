using Core.Server.Shared.Query;
using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using Core.Server.Tests.ResourceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Unity;

namespace Core.Server.Test.ResourcesTests.Implementation
{
    [TestClass]
    public class QeuryExmapleTetst
         : GenericExtraTestBase<ExampleCreateResource, ExampleUpdateResource, ExampleResource>
    {
        [Dependency]
        public IResourceQuery<ExampleResource> ResourceQuery;

        protected override object GetThis() => this;

        [TestMethod]
        public void TestQueryEquals()
        {
            CreateResource();
            var queryPhrase = $"name.equals({CreatedResource.Name})";
            var result =  ResourceQuery.Query(new QueryResource() { QueryPhrase = queryPhrase });
            AssertAreEqual(CreatedResource,result);
        }

        [TestMethod]
        public void TestQueryOrderBy()
        {
            var resources = CreateResources(2).OrderBy(r => r.Value).ToList();
            var queryPhrase = $"value > 0";

            var result = ResourceQuery.Query(new QueryResource() { QueryPhrase = queryPhrase, OrderBy = "value" });

            ValidateList(resources, result.Value);
        }

        [TestMethod]
        public void TestQueryOrderByDecending()
        {
            var resources = CreateResources(2).OrderByDescending(r => r.Value).ToList();
            var queryPhrase = $"value > 0";

            var result = ResourceQuery.Query(new QueryResource() { QueryPhrase = queryPhrase, OrderByDecending = "value" });

            ValidateList(resources, result.Value);
        }
    }
}
