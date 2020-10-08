using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Server.Common
{
    public class InjectWithNameAttribute : Attribute
    {
        public InjectWithNameAttribute(Type type)
        {
            this.Type = type;
        }

        public Type Type { get; }
    }
}
