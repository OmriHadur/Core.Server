using Core.Server.Application.Helpers;
using Core.Server.Common;
using Core.Server.Common.Attributes;
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
            AddInjectedContollers(feature);
            AddboundleContollers(feature);
        }

        private void AddboundleContollers(ControllerFeature feature)
        {
            var contollersType = typeof(BatchController<,,,>);
            var resourcesBoundles = reflactionHelper.GetResourcesBoundles().ToList();

            foreach (var resourcesBoundle in resourcesBoundles)
                AddContoller(feature, resourcesBoundle, contollersType);
        }

        private void AddInjectedContollers(ControllerFeature feature)
        {
            var controllerTypes = reflactionHelper.GetTypesWithAttribute<InjectControllerAttribute>();
            foreach (var controllerType in controllerTypes)
                feature.Controllers.Add(controllerType.GetTypeInfo());
        }

        private void AddContoller(ControllerFeature feature, ResourceBoundle resourceBoundle, Type controllerType)
        {
            var controllerGenericType = reflactionHelper.FillGenericType(controllerType, resourceBoundle);
            var hasDrivenContoller = HasSameContoller(feature, controllerGenericType);
            if (!hasDrivenContoller)
                feature.Controllers.Add(controllerGenericType.GetTypeInfo());
        }

        private bool HasSameContoller(ControllerFeature feature, Type controllerGenericType)
        {
            foreach (var cotroller in feature.Controllers)
            {
                var firstGenericArgument = cotroller.BaseType.GetGenericArguments().First();
                var firstControllerGenericArgument = controllerGenericType.GetGenericArguments().First();
                if (firstGenericArgument == firstControllerGenericArgument)
                    return true;
            }
            return false;
        }
    }
}
