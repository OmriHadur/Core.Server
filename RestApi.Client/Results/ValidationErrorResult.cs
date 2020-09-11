using System;
using System.Collections.Generic;
using System.Text;

namespace RestApi.Standard.Client.Results
{
    public class ValidationErrorResult : ActionResult
    {
        public string Title { get; set; }

        public string TraceId { get; set; }

        public Dictionary<string, string[]> Errors { get; set; }
    }
}
