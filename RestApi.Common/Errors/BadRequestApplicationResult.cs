using Microsoft.AspNetCore.Mvc;
using RestApi.Shared.Errors;

namespace RestApi.Common.Errors
{
    public class BadRequestApplicationResult : BadRequestObjectResult
    {
        public BadRequestApplicationResult(BadRequestReason reason) 
            : base(reason)
        {

        }
    }
}
