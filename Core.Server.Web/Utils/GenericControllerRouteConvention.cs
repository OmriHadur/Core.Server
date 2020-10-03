using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Server.Web.Utils
{
    public class GenericControllerRouteConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.IsGenericType)
            {
                var createResourceType = controller.ControllerType.GetGenericArguments().First();
                var route = createResourceType.Name.Replace(createResourceType.BaseType.Name, string.Empty);
                controller.Selectors.Add(new SelectorModel
                {
                    AttributeRouteModel = new AttributeRouteModel() { Template = route },
                });
            }
        }
    }
}
