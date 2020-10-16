using Core.Server.Injection.Attributes;
using Core.Server.Injection.Interfaces;
using Core.Server.Injection.Unity;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Core.Server.Web.Utils
{
    public class GenericTypeControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private readonly IReflactionHelper reflactionHelper;

        public GenericTypeControllerFeatureProvider(IReflactionHelper reflactionHelper)
        {
            this.reflactionHelper = reflactionHelper;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            //AddInjectedContollers(feature);
            AddboundleContollers(feature);
        }

        private void AddboundleContollers(ControllerFeature feature)
        {
            var controllerTypes = reflactionHelper.GetGenericTypesWithAttribute<InjectBoundleControllerAttribute>();
            var resourcesBoundles = reflactionHelper.GetResourcesBoundles().ToList();

            foreach (var resourcesBoundle in resourcesBoundles)
                foreach (var controllerType in controllerTypes)
                    AddContoller(feature, resourcesBoundle, controllerType);
        }

        private void AddContoller(ControllerFeature feature, ResourceBoundle resourceBoundle, Type controllerType)
        {
            var controllerGenericType = reflactionHelper.FillGenericType(controllerType, resourceBoundle);
            var hasDrivenContoller = HasSameContoller(feature, controllerGenericType);
            if (!hasDrivenContoller)
                feature.Controllers.Add(controllerGenericType.GetTypeInfo());
        }

        private bool HasSameContoller(ControllerFeature feature, Type controllerType)
        {
            return feature.Controllers.Any(c => reflactionHelper.IsSameType(c, controllerType));
        }
    }
}
