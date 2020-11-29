using MongoDB.Driver;
using Core.Server.Common;
using Core.Server.Common.Entities;
using Core.Server.Common.Repositories;
using System.Security.Authentication;
using Unity;
using Core.Server.Common.Config;

namespace Core.Server.Persistence.Repositories
{
    public class BaseRepository<TEntity> 
        : IBaseRepository
        where TEntity : Entity
    {
        protected IMongoCollection<TEntity> Collection;

        [Dependency]
        public MongoDBConfig MongoDatabaseSettings
        {
            set
            {
                var settings = MongoClientSettings.FromUrl(new MongoUrl(value.ConnectionString));
                settings.SslSettings =
                    new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
                var client = new MongoClient(settings);
                var database = client.GetDatabase(value.Database);
                Collection = database.GetCollection<TEntity>(CollectionName);
            }
        }

        protected string CollectionName
        {
            get { return typeof(TEntity).Name.Replace(nameof(Entity), string.Empty); }
        }
    }
}
