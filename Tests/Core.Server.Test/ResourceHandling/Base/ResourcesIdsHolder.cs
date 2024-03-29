﻿using Core.Server.Common.Attributes;
using Core.Server.Test.ResourceCreators.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Server.Test.ResourcesCreators.Infrastructure
{
    [Inject]
    public class ResourcesIdsHolder : IResourcesIdsHolder
    {
        private readonly Dictionary<Type, List<string>> idsbyType;

        public ResourcesIdsHolder()
        {
            idsbyType = new Dictionary<Type, List<string>>();
        }

        public void Add<TResource>(string id)
        {
            if (id == null) return;
            var type = typeof(TResource);
            if (!idsbyType.ContainsKey(type))
                idsbyType.Add(type, new List<string>());
            idsbyType[type].Add(id);
        }

        public bool Contains<TResource>(string id)
        {
            var type = typeof(TResource);
            return idsbyType.ContainsKey(type) && idsbyType[type].Contains(id);
        }

        public void Clean<TResource>()
        {
            var type = typeof(TResource);
            if (idsbyType.ContainsKey(type))
                idsbyType.Remove(type);
        }

        public IEnumerable<string> GetAll<TResource>()
        {
            var type = typeof(TResource);
            if (idsbyType.ContainsKey(type))
                return idsbyType[type];
            return new string[0];
        }

        public IEnumerable<Type> GetAllTypes()
        {
            return idsbyType.Keys;
        }

        public string GetLast<TResource>()
        {
            var type = typeof(TResource);
            if (idsbyType.ContainsKey(type))
                return idsbyType[type].Last();
            return null;
        }

        public bool IsEmpty<TResource>()
        {
            var type = typeof(TResource);
            if (idsbyType.ContainsKey(type))
                return idsbyType[type].Count == 0;
            return true;
        }

        public void Remove<TResource>(string id)
        {
            var type = typeof(TResource);
            if (idsbyType.ContainsKey(type))
            {
                idsbyType[type].Remove(id);
                if (idsbyType[type].Count == 0)
                    idsbyType.Remove(type);
            }
        }
    }
}
