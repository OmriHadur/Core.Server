using System;

namespace Core.Server.Common.Config
{
    [Flags]
    public enum LoggingActions : short
    {
        None = 0,
        Started = 1,
        Finished = 2,
        Took = 4,
        Request=8,
        Response= 16
    }
}