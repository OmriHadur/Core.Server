
namespace Core.Server.Common.Config
{
    public class CacheConfig
    {
        public int MaxEntities { get; set; }

        public int MaxQueries { get; set; }

        public CacheTypeConfig[] Overrides { get; set; }

        public string[] Preload { get; set; }

        public string[] Exclude { get; set; }
    }
}
