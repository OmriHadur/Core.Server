using RestApi.Standard.Shared.Errors;

namespace RestApi.Standard.Client.Results
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
