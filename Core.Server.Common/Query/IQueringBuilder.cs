using Core.Server.Common.Query;
using Core.Server.Shared.Query;
using Core.Server.Shared.Resources;

namespace Core.Server.Application.Query
{
    public interface IQueringBuilder
    {
        QueryBase Build<TResource>(QueryResource queryResource)
            where TResource : Resource;
    }
}