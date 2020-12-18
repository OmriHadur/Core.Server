using Core.Server.Shared.Resources;
using Core.Server.Shared.Resources.Users;
using System;

namespace Core.Server.Tests.Utils
{
    public interface ICurrentUser
    {
        event EventHandler<string> OnTokenChange;
        string Token { get; }
        UserResource UserResource { get; }
        void LoginAsAdmin();
        void LoginAs(string email);
        void ReLogin();
        void Logout();
        void AddRole(Type type, ResourceActions resourceActions);
    }
}