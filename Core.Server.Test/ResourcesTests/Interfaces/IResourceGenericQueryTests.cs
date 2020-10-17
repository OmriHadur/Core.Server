namespace Core.Server.Tests.ResourceTests.Interfaces
{
    public interface IResourceGenericQueryTests
        : IResourceGenericTests
    {
        void TestCreateAddedToList();
        void TestGet();
        void TestGetNotFound();
        void TestList();
    }
}