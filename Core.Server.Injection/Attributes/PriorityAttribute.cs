using System;

namespace Core.Server.Injection.Attributes
{
    public class PriorityAttribute : Attribute
    {
        public int Priority { get; set; }

        public PriorityAttribute(int priority)
        {
            Priority = priority;
        }
    }
}