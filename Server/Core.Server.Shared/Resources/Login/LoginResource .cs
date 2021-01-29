
namespace Core.Server.Shared.Resources
{
    public class LoginResource : Resource
    {
        public string Token { get; set; }

        public UserResource User { get; set; }
    }
}
