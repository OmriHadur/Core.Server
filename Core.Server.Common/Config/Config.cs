using Core.Server.Shared.Resources;

namespace Core.Server.Common.Config
{
    public class Config
    {
        public AppConfig AppSettings { get; set; }
        public MongoDBConfig MongoDB { get; set; }
        public CacheConfig Cache { get; set; }
        public LoggingConfig Logging { get; set; }
        public PolicyResource[] AllowAnonymous { get; set; }
        public string[] Assemblies { get; set; }
    }
}
