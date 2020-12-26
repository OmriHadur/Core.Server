using Core.Server.Injection.Interfaces;
using Core.Server.Injection.Reflaction;
using Core.Server.Shared.Resources;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Linq;
using Unity;

namespace Core.Server.Web.Utils
{
    public class GenericControllerRouteConvention : IControllerModelConvention
    {
        [Dependency]
        public IReflactionHelper ReflactionHelper;

        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.IsGenericType)
            {
                var createResourceType = controller.ControllerType.GetGenericArguments().First();
                var route = GetRoute(createResourceType);
                controller.Selectors.Add(new SelectorModel
                {
                    AttributeRouteModel = new AttributeRouteModel() { Template = route },
                });
            }
        }

        private static string GetRoute(Type createResourceType)
        {
            if (createResourceType.BaseType == typeof(ChildAlterResource))
            {
                var parentName = GetFirstWord(createResourceType.Name);
                var childName = GetNameRemoveCreateResource(createResourceType).Replace(parentName, string.Empty);
                return parentName + "/" + childName;
            }
            else
                return GetNameRemoveCreateResource(createResourceType);
        }

        private static string GetNameRemoveCreateResource(Type createResourceType)
        {
            return createResourceType.Name
                .Replace("CreateResource", string.Empty)
                .Replace("AlterResource", string.Empty)
                .Replace(nameof(Resource), string.Empty);
        }

        private static string GetFirstWord(string str)
        {
            for (int i = 1; i < str.Length; i++)
                if (char.IsUpper(str[i]))
                    return str.Substring(0, i);
            return str;
        }
    }
}
