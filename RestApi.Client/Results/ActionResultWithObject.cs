using System;
using System.Collections.Generic;
using System.Text;

namespace RestApi.Client.Results
{
    public class ActionResult<T> : ActionResult
    {
        public ActionResult Result { get; protected set; }
        public T Value { get; private set; }

        public ActionResult(ActionResult result)
        {
            Result = result;
        }
        public ActionResult(T value)
        {
            Value = value;
        }
        
    }
}
