using Core.Server.Client.Results;
using Core.Server.Shared.Query;
using Core.Server.Shared.Resources;
using Core.Server.Test.ResourceCreators.Interfaces;
using Core.Server.Test.ResourceTests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace Core.Server.Test.ResourcesTests.Implementation
{
    [TestClass]
    public class ScaleTests
         : GenericExtraTestBase<ExampleCreateResource, ExampleUpdateResource, ExampleResource>
    {
        protected override object GetThis() => this;

        [TestMethod]
        public void TestQueryEquals()
        {
            CreateResource();

            var result = Query(GetQueryPhraseEquals());

            AssertAreEqual(CreatedResource, result);
        }

        [TestMethod]
        public void TestQueryOrderBy()
        {
            var resources = CreateResources(2).OrderBy(r => r.Value).ToList();

            var result = Query(orderBy: nameof(ExampleResource.Value));

            ValidateList(resources, result.Value);
        }

        [TestMethod]
        public void TestQueryOrderByDecending()
        {
            var resources = CreateResources(2).OrderByDescending(r => r.Value).ToList();

            var result = Query(orderByDecending: nameof(ExampleResource.Value));

            ValidateList(resources, result.Value);
        }

        [TestMethod]
        public void TestQueryGreater()
        {
            var resources = CreateResources(2).OrderBy(r => r.Value).ToList();
            var queryPhrase = nameof(ExampleResource.Value) + " >= " + resources.Last().Value;

            var result = Query(queryPhrase);

            AssertAreEqual(resources.Last(), result);
        }

        [TestMethod]
        public void TestQueryPage()
        {
            var res1 = CreateResource();
            var res2 = ResourceAlter.Create(r => r.Name = CreatedResource.Name).Value;

            var result = Query(queryPhrase: GetQueryPhraseEquals(), orderBy: nameof(ExampleResource.Value), page: 2, pageSize: 1);

            var expected = res1.Value < res2.Value ? res2 : res1;
            Assert.AreEqual(1, result.Value.Count());
            Validate(expected, result.Value.First());
        }

        private ActionResult<IEnumerable<ExampleResource>> Query(
            string queryPhrase = nameof(ExampleResource.Value) + " > 0",
            string orderBy = null,
            string orderByDecending = null,
            int page = 0,
            int pageSize = 0)
        {
            return ResourceQuery.Query(new QueryResource()
            {
                QueryPhrase = queryPhrase,
                OrderBy = orderBy,
                OrderByDescending = orderByDecending,
                Page = page,
                PageSize = pageSize
            });
        }

        private string GetQueryPhraseEquals()
        {
            return $"{nameof(ExampleResource.Name)}.equals({CreatedResource.Name})";
        }
    }
}
