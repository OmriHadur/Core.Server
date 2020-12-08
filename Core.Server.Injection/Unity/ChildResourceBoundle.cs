using Core.Server.Common.Entities;
using Core.Server.Injection.Interfaces;
using Core.Server.Shared.Resources;
using System;

namespace Core.Server.Injection.Unity
{
    public class ChildResourceBoundle : ResourceBoundle
    {
        public ChildResourceBoundle(Type resourceType, IReflactionHelper reflactionHelper)
            :base(resourceType,reflactionHelper)
        {
            var parentName = GetFirstWord(ResourceName);
            Insert(0,reflactionHelper.GetTypeWithPrefix<Entity>(parentName));
            Insert(0,reflactionHelper.GetTypeWithPrefix<Resource>(parentName));
        }

        private string GetFirstWord(string str)
        {
            for (int i = 1; i < str.Length; i++)
                if (char.IsUpper(str[i]))
                    return str.Substring(0, i);
            return str;
        }
    }
}
