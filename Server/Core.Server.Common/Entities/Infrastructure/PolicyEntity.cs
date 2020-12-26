namespace Core.Server.Common.Entities
{
    public class PolicyEntity : Entity
    {
        public string Name { get; set; }

        public string ResourceType { get; set; }

        public short ResourceActions { get; set; }
    }
}
