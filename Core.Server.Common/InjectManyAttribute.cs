using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Server.Common
{
    public class InjectManyAttribute : Attribute
    {
        public InjectManyAttribute(Type type)
        {
            this.Type = type;
        }

        public Type Type { get; }
    }
}
