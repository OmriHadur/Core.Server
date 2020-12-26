using Core.Server.Shared.Resources.Users;

namespace Core.Server.Common.Helpers
{
    public interface ICurrentUserGetter
    {
        UserResource CurrentUser { get; set; }
    }
}
