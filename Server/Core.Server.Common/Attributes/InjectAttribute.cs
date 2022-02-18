namespace Core.Server.Common.Attributes
{
    public class InjectAttribute : PriorityAttribute
    {
        public InjectAttribute(int priority = 1)
            : base(priority)
        {
        }
    }
}