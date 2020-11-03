namespace Core.Server.Common.Cache
{
    public class EntityCacheChangedEventArgs
    {
        public string Id { get; set; }
        public bool IsAltered { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsClear { get; set; }
    }
}
