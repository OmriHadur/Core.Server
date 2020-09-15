using Microsoft.AspNetCore.Mvc;
using RestApi.Shared.Errors;

namespace RestApi.Common.Errors
{
    public class BadRequestApplicationResult : BadRequestObjectResult
    {
        public BadRequestApplicationResult(int reasonNumber, string description)
            : base(new { Reason = reasonNumber, Description = description })
        {
        }
    }
}
