namespace Core.Server.Test.ResourceTests.Interfaces
{
    public interface IResourceGenericRestTests
        : IResourceGenericTests
    {
        void TestCreateAddedToList();
        void TestDelete();
        void TestDeleteNotFoundAfterDelete();
        void TestGet();
        void TestGetNotFound();
        void TestGetNotFoundAfterDelete();
    }
}