using RestApi.Common.Entities;
using System.Threading.Tasks;

namespace RestApi.Common.Repositories
{
    public interface ILoginRepository : IRepository<LoginEntity>
    {
        Task<LoginEntity> GetByUserId(string id);
        Task DeleteByUserId(string id);
    }
}
