using Core.Server.Client.Results;

namespace Core.Server.Client.Results
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