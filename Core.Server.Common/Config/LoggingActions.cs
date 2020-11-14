using System;

namespace Core.Server.Common.Config
{
    [Flags]
    public enum LoggingActions : short
    {
        None,
        Method,//only write start and fininsh of methods
        MethodsTime, // also write how long it took
        MethodsTimeInputOutput, // also write the input and output of methods
    }
}