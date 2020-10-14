namespace Core.Server.Client.Results
{
    public class ActionResult<T> : ActionResult
    {
        public ActionResult Result { get; protected set; }
        public T Value { get; private set; }

        public ActionResult(ActionResult result)
        {
            Result = result;
        }
        public ActionResult(T value)
        {
            Value = value;
        }

        public bool IsSuccess => Value != null;
        public bool IsFail => Result != null;

    }
}
