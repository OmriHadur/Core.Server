using Core.Server.Client.Results;

namespace Core.Server.Tests.ResourceCreators.Interfaces
{
    public interface IResourceDeleter
    {
        ActionResult Delete(string id);
        void DeleteAll();
    }
}
