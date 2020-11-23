using System;
using System.Collections.Generic;

namespace Core.Server.Tests.ResourceCreators.Interfaces
{
    public interface IResourcesIdsHolder
    {
        bool IsEmpty<TResource>();

        string GetLast<TResource>();

        IEnumerable<string> GetAll<TResource>();

        IEnumerable<Type> GetAllTypes();

        void Add<TResource>(string id);

        bool Contains<TResource>(string id);

        void Remove<TResource>(string id);

        void Clean<TResource>();
    }
}