using Core.Server.Shared.Resources;
using Core.Server.Shared.Resources.Users;
using System;

namespace Core.Server.Test.Utils
{
    public interface ICurrentUser
    {
        event EventHandler<string> OnTokenChange;
        string Token { get; }
        UserResource UserResource { get; }
        void LoginAsAdmin();
        void Login();
        void LoginAs(string email);
        void Logout();
        void AddRoleAndRelogin(Type type, ResourceActions resourceActions);
    }
}