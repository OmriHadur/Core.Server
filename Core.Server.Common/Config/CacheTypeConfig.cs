namespace Core.Server.Common.Config
{
    public class CacheTypeConfig
    {
        public string Type { get; set; }
        public int MaxEntities { get; set; }
        public int MaxQueries { get; set; }
    }
}
