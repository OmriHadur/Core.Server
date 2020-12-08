using Core.Server.Common.Attributes;
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
            AddBoundleContollers(feature);
            AddChildBoundleContollers(feature);
        }

        private void AddBoundleContollers(ControllerFeature feature)
        {
            var controllerTypes = reflactionHelper.GetGenericTypesWithAttribute<InjectBoundleControllerAttribute>();
            var resourcesBoundles = reflactionHelper.GetResourcesBoundles();

            foreach (var resourcesBoundle in resourcesBoundles)
                foreach (var controllerType in controllerTypes)
                    AddContoller(feature, resourcesBoundle, controllerType);
        }

        private void AddChildBoundleContollers(ControllerFeature feature)
        {
            var controllerTypes = reflactionHelper.GetGenericTypesWithAttribute<InjectChildBoundleControllerAttribute>();
            var resourcesBoundles = reflactionHelper.GetChildResourcesBoundles();

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