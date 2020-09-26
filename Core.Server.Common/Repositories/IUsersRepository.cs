using Core.Server.Common.Entities;
using System.Threading.Tasks;

namespace Core.Server.Common.Repositories
{
    public interface IUsersRepository : IRepository<UserEntity>
    {
        Task<bool> EmailExists(string email);

        Task<UserEntity> GetByEmail(string email);
    }
}
