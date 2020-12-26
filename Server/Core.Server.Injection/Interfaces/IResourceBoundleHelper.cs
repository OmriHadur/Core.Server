using Core.Server.Injection.Unity;
using System;
using System.Collections.Generic;

namespace Core.Server.Injection.Interfaces
{
    public interface IResourceBoundleHelper
    {
        IEnumerable<ResourceBoundle> GetAllResourcesBoundles();
        IEnumerable<ResourceBoundle> GetResourcesBoundles();
        IEnumerable<ResourceBoundle> GetChildResourcesBoundles();
        Type GetTypeWithPrefix<T>(string prefix);
        string GetTypeName(Type drivenType, Type subType);
    }
}