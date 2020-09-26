using Microsoft.AspNetCore.Mvc;
using Core.Server.Shared.Errors;

namespace Core.Server.Common.Errors
{
    public class BadRequestApplicationResult : BadRequestObjectResult
    {
        public BadRequestApplicationResult(int reasonNumber, string description)
            : base(new { Reason = reasonNumber, Description = description })
        {
        }
    }
}
