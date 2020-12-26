using Core.Server.Shared.Errors;

namespace Core.Server.Client.Results
{
    public class BadRequestResult : ActionResult
    {
        public int Reason { get; private set; }
        public BadRequestResult(int reason)
        {
            Reason = reason;
        }
    }
}
