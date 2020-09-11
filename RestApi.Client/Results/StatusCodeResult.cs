using RestApi.Standard.Client.Results;

namespace RestApi.Standard.Client.Results
{
    public class StatusCodeResult : ActionResult
    {
        public int StatusCode { get; protected set; }

        public StatusCodeResult(int statusCode)
        {
            this.StatusCode = statusCode;
        }
    }
}