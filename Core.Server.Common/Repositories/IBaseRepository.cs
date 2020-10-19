namespace Core.Server.Common.Repositories
{
    public interface IBaseRepository
    {
        IMongoDBConfig MongoDatabaseSettings { set; }
    }
}