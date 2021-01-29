using Core.Server.Shared.Resources;

namespace Core.Server.Common.Helpers
{
    public interface ICurrentUserGetter
    {
        UserResource CurrentUser { get; set; }
    }
}
