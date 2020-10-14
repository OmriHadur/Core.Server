using Core.Server.Shared.Resources;
using Core.Server.Tests.ResourceCreators.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Core.Server.Test.ResourcesCreators.Infrastructure
{
    public class ResourceIdHolder<TResouce>
        : IResourceIdsHolder<TResouce>
        where TResouce : Resource
    {
        private readonly List<string> ids;

        public ResourceIdHolder()
        {
            ids = new List<string>();
        }

        public void Add(string id)
        {
            ids.Add(id);
        }

        public void Clean()
        {
            ids.Clear();
        }

        public IEnumerable<string> GetAll()
        {
            return ids;
        }

        public string GetLast()
        {
            return ids.Last();
        }

        public bool IsEmpty()
        {
            return ids.Count == 0;
        }

        public void Remove(string id)
        {
            ids.Remove(id);
        }
    }
}
