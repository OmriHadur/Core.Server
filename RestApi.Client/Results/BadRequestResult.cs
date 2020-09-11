using RestApi.Shared.Errors;

namespace RestApi.Client.Results
{
    public class BadRequestResult : ActionResult
    {
        public BadRequestReason Reason { get; private set; }
        public BadRequestResult(BadRequestReason reason)
        {
            Reason = reason;
        }
    }
}
