using Core.Server.Shared.Resources;

namespace Core.Server.Common.Config
{
    public class Config
    {
        public AppSettings AppSettings { get; set; }
        public MongoDBConfig MongoDB { get; set; }
        public CacheConfig Cache { get; set; }
        public LoggingConfig Logging { get; set; }
        public PolicyResource[] AllowAnonymous { get; set; }
        public ResourceBoundle[] ResourceBoundles { get; set; }
        public ChildResourceBoundle[] ChildResourceBoundles { get; set; }
        public string[] AssembliesPrefixes { get; set; }
    }
}
