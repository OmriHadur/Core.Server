using RestApi.Shared.Errors;

namespace RestApi.Client.Results
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
