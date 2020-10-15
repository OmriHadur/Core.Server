using Core.Server.Tests.ResourceCreators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Server.Test.ResourcesCreators.Infrastructure
{
    public class ResourceIdHolder: IResourceIdsHolder
    {
        private readonly Dictionary<Type, List<string>> idsbyType;

        public ResourceIdHolder()
        {
            idsbyType = new Dictionary<Type, List<string>>();
        }

        public void Add<T>(string id)
        {
            var type = typeof(T);
            if (!idsbyType.ContainsKey(type))
                idsbyType.Add(type, new List<string>());
            idsbyType[type].Add(id);
        }

        public void Clean<T>()
        {
            var type = typeof(T);
            if (idsbyType.ContainsKey(type))
                idsbyType.Remove(type);
        }

        public IEnumerable<string> GetAll<T>()
        {
            var type = typeof(T);
            if (idsbyType.ContainsKey(type))
                return idsbyType[type];
            return new string[0];
        }

        public IEnumerable<Type> GetAllTypes()
        {
            return idsbyType.Keys;
        }

        public string GetLast<T>()
        {
            var type = typeof(T);
            if (idsbyType.ContainsKey(type))
                idsbyType[type].Last();
            return string.Empty;
        }

        public bool IsEmpty<T>()
        {
            var type = typeof(T);
            if (idsbyType.ContainsKey(type))
                return idsbyType[type].Count == 0;
            return true;
        }

        public void Remove<T>(string id)
        {
            var type = typeof(T);
            if (idsbyType.ContainsKey(type))
                idsbyType[type].Remove(id);
            if (idsbyType[type].Count == 0)
                idsbyType.Remove(type);
        }
    }
}
