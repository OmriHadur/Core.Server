using Core.Server.Shared.Resources;
using System;
using System.Collections.Generic;

namespace Core.Server.Tests.ResourceCreators.Interfaces
{
    public interface IResourceIdsHolder
    {
        bool IsEmpty<T>();

        string GetLast<T>();

        IEnumerable<string> GetAll<T>();

        IEnumerable<Type> GetAllTypes();

        void Add<T>(string id);

        void Remove<T>(string id);

        void Clean<T>();
    }
}