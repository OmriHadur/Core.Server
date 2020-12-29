using Core.Server.Common.Attributes;
using Core.Server.Injection.Interfaces;
using Core.Server.Shared.Resources;
using Core.Server.Shared.Resources.User;
using Core.Server.Test.Configuration;
using Core.Server.Test.ResourceCreators.Interfaces;
using System;
using Unity;

namespace Core.Server.Test.Utils
{
    [Inject]
    public class CurrentUser : ICurrentUser
    {
        private string token;
        private bool isLoggin;

        [Dependency]
        public TestConfig Config;

        [Dependency]
        public Lazy<IResourceAlter<UserAlterResource, UserResource>> UserResourceAlter;

        [Dependency]
        public Lazy<IResourceCreate<UserResource>> UserResourceCreate;

        [Dependency]
        public Lazy<IResourceAlter<LoginAlterResource, LoginResource>> LoginResourceAlter;

        [Dependency]
        public Lazy<IResourceAlter<PolicyAlterResource, PolicyResource>> PolicyResourceAlter;

        [Dependency]
        public Lazy<IResourceCreate<RoleResource>> RoleResourceCreate;

        [Dependency]
        public IReflactionHelper ReflactionHelper;

        public event EventHandler<string> OnTokenChange;

        public UserResource UserResource { get; private set; }

        public string Token
        {
            get
            {
                if (token == null && !isLoggin)
                    LoginAsAdmin();
                return token;
            }
        }

        public void LoginAsAdmin()
        {
            isLoggin = true;
            var login = LoginResourceAlter.Value.Create(new LoginAlterResource()
            {
                Email = Config.AdminUser,
                Password = Config.AdminPassword
            }).Value;
            token = login.Token;
            UserResource = login.User;
            OnTokenChange?.Invoke(this, token);
            isLoggin = false;
        }

        public void Logout()
        {
            token = null;
            UserResource = null;
            OnTokenChange?.Invoke(this, token);
        }

        public void Login()
        {
            LoginAsAdmin();
            var userResource = UserResourceCreate.Value.GetOrCreate();
            var email = userResource.Email;
            LoginAs(email);
        }

        public void LoginAs(string email)
        {
            var login = LoginResourceAlter.Value.Create(cr => cr.Email = email).Value;
            token = login.Token;
            UserResource = login.User;
            OnTokenChange?.Invoke(this, token);
        }

        public void AddRoleAndRelogin(Type type, ResourceActions resourceActions)
        {
            LoginAsAdmin();
            PolicyResourceAlter.Value.Create(p =>
            {
                p.ResourceType = ReflactionHelper.GetTypeFullName(type);
                p.ResourceActions = resourceActions;
            });
            var roleId = RoleResourceCreate.Value.Create().Value.Id;
            //UserResourceAlter.Value.Replace(ur => ur.RolesIds = new string[] { roleId });
            Login();
        }
    }
}
