using Microsoft.AspNetCore.Mvc;
using RestApi.Standard.Shared.Errors;

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
