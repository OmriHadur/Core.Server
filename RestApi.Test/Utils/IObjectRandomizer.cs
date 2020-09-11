namespace RestApi.Tests.Utils
{
    public interface IObjectRandomizer
    {
        void AddRandomValues(object resource);
        string GetRandomId();
        string GetRandomString(int length);
    }
}