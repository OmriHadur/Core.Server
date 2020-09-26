using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Core.Server.Common.Errors
{
    public class ValidationErrorResult : ActionResult
    {
        public string Title { get; set; }

        public string TraceId { get; set; }

        public Dictionary<string, string[]> Errors { get; set; }
    }
}
