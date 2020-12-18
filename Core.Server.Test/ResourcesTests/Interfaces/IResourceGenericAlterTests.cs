namespace Core.Server.Test.ResourceTests.Interfaces
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