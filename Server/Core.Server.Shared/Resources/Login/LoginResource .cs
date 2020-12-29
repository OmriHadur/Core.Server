
namespace Core.Server.Shared.Resources.User
{
    public class LoginResource : Resource
    {
        public string Token { get; set; }

        public UserResource User { get; set; }
    }
}
