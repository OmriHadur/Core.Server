using Core.Server.Common.Entities;
using System.Threading.Tasks;

namespace Core.Server.Common.Repositories
{
    public interface ILoginsRepository  : IRepository<LoginEntity>
    {
        Task<LoginEntity> GetByUserId(string id);
        Task DeleteByUserId(string id);
    }
}
