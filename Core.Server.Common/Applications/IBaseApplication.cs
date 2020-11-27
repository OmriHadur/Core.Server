using Core.Server.Common.Helpers;

namespace Core.Server.Common.Applications
{
    public interface IBaseApplication
    {
        ICurrentUserGetter CurrentUserGetter { get; }
    }
}