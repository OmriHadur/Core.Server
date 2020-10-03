using Core.Server.Shared.Resources.Users;

namespace Core.Server.Common.Applications
{
    public interface IApplicationBase
    {
        public UserResource CurrentUser { get; set; }
    }
}