using System;
namespace Core.Server.Tests.Utils
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