
using System;

namespace Core.Server.Shared.Resources
{
    public class PolicyResource : Resource
    {
        public string Name { get; set; }

        public string ResourceType { get; set; }

        public ResourceActions ResourceActions { get; set; }
    }
}