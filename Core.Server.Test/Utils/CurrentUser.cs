using Core.Server.Common.Attributes;
using Core.Server.Shared.Resources.Users;
using Core.Server.Tests.Configuration;
using Core.Server.Tests.ResourceCreators.Interfaces;
using System;
using Unity;

namespace Core.Server.Tests.Utils
{
    [Inject]
    public class CurrentUser : ICurrentUser
    {
        private string token;
        private bool logginin;

        [Dependency]
        public IResourcesIdsHolder ResourcesHolder;

        [Dependency]
        public TestConfig Config;

        [Dependency]
        public Lazy<IResourceCreate<UserResource>> UserResourceCreate;

        [Dependency]
        public Lazy<IResourceCreate<LoginResource>> LoginResourceCreate;

        [Dependency]
        public Lazy<IResourceAlter<LoginCreateResource, LoginUpdateResource,LoginResource>> LoginResourceAlter;

        public event EventHandler<string> OnTokenChange;
        public string Email { get; private set; }

        public string Token
        {
            get
            {
                if (token == null && !logginin)
                    Login();
                return token;
            }
        }

        public void Relogin()
        {
            UserResourceCreate.Value.Create();
            Logout();          
            Login();
        }

        public void Login()
        {
            if (!string.IsNullOrEmpty(token) | logginin)
                return;

            logginin = true;
            Email = UserResourceCreate.Value.GetOrCreate().Email;
            var login = LoginResourceCreate.Value.GetOrCreate();
            token = login.Token;
            logginin = false;
            OnTokenChange?.Invoke(this, token);
        }




        public void Logout()
        {
            var currentLogin = LoginResourceCreate.Value.GetIfExist();
            if (currentLogin != null)
                LoginResourceCreate.Value.Delete(currentLogin.Id);
            token = null;
            OnTokenChange?.Invoke(this, token);
        }

        public void LoginAs(string email)
        {
            var login = LoginResourceAlter.Value.Create(cr => cr.Email = email).Value;
            token = login.Token;
            OnTokenChange?.Invoke(this, token);
        }
    }
}
