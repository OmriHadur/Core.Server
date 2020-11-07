using System;

namespace Core.Server.Injection.Attributes
{
    public class InjectAttribute : PriorityAttribute
    {
        public InjectAttribute(int priority = 1)
            : base(priority)
        {
        }
    }
}