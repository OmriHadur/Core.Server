using System;
namespace RestApi.Tests.Utils
{
    public interface ITokenHandler
    {
        event EventHandler<string> OnTokenChange;
        string Token { get; }
        void Login();
        void LoginWithNewUser();
        void Logout();
    }
}