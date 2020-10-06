using Core.Server.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Core.Server.Web.Utils
{
    public class GenericTypeControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private IReflactionHelper reflactionHelper;
        public GenericTypeControllerFeatureProvider(IReflactionHelper reflactionHelper)
        {
            this.reflactionHelper = reflactionHelper;
        }
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var controllerTypes = reflactionHelper.GetDrivenTypesOf<BaseController>().ToList();
            var resourcesBoundles = reflactionHelper.GetResourcesBoundles().ToList();

            foreach (var resourcesBoundle in resourcesBoundles)
                foreach (var controllerType in controllerTypes)
                    AddContoller(feature, resourcesBoundle, controllerType);
        }

        private void AddContoller(ControllerFeature feature, ResourceBoundle resourceBoundle, Type controllerType)
        {
            var controllerGenericType = reflactionHelper.MakeGenericType(controllerType, resourceBoundle);
            var hasDrivenContoller = HasSameContoller(feature, controllerGenericType);
            if (!hasDrivenContoller)
                feature.Controllers.Add(controllerGenericType.GetTypeInfo());
        }

        private bool HasSameContoller(ControllerFeature feature, Type controllerGenericType)
        {
            var typeInfo = feature.Controllers.FirstOrDefault(ti => ti.BaseType == controllerGenericType);
            return typeInfo != null;
        }
    }
}
