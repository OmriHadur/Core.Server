namespace Core.Server.Tests.ResourceTests.Interfaces
{
    public interface IResourceGenericAlterTests
        : IResourceGenericTests
    {
        void TestReplace();

        void TestReplaceCreated();

        void TestUpdate();

        void TestGetAfterUpdate();

    }
}