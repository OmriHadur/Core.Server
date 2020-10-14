using Core.Server.Shared.Resources;
using System.Collections.Generic;

namespace Core.Server.Tests.ResourceCreators.Interfaces
{
    public interface IResourceIdsHolder<TResouce>
        where TResouce:Resource
    {
        bool IsEmpty();

        string GetLast();

        IEnumerable<string> GetAll();

        void Add(string id);

        void Remove(string id);

        void Clean();
    }
}