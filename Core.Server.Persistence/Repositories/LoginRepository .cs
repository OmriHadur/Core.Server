using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using Core.Server.Common;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Core.Server.Persistence.Repositories
{
    [Inject]
    public class LoginsRepository : 
        RestRepository<LoginEntity>, 
        ILoginRepository 
    {
    }
}