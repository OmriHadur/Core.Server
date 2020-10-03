using Microsoft.Extensions.Configuration;
using Core.Server.Application;
using Core.Server.Common;
using Core.Server.Shared.Resources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Unity;
using Core.Server.Persistence.Repositories;

namespace Core.Server.Web.Utils
{
    public class UnityContainerBuilder
    {
        public void ConfigureContainer(IUnityContainer container, IReflactionHelper reflactionHelper)
        {
            var resourcesBoundles = reflactionHelper.GetResourcesBoundles();

            foreach (var resourcesBoundle in resourcesBoundles)
            {
                foreach (var genricTypeForBundle in GetGenricTypesForBundle(reflactionHelper))
                {
                    var genericType = reflactionHelper.MakeGenericType(genricTypeForBundle, resourcesBoundle);
                    AddTypesInterfaces(container, genericType);
                }
            }

            AddInjectTypes(container, reflactionHelper);
            AddInjectManyClasses(container, reflactionHelper);
        }

        private static IEnumerable<Type> GetGenricTypesForBundle(IReflactionHelper reflactionHelper)
        {
            var applicationTypes = reflactionHelper.GetDrivenTypesOf<BaseApplication>();
            var repositoryTypes = reflactionHelper.GetDrivenTypesOf(typeof(BaseRepository<>));
            return applicationTypes.Union(repositoryTypes);
        }

        private void AddInjectTypes(IUnityContainer container, IReflactionHelper reflactionHelper)
        {
            var classTypes = reflactionHelper.GetTypesWithAttribute<InjectAttribute>();
            foreach (var injectedType in classTypes)
                AddTypesInterfaces(container, injectedType);
        }

        private void AddInjectManyClasses(IUnityContainer container, IReflactionHelper reflactionHelper)
        {
            var classTypes = reflactionHelper.GetTypesWithAttribute<InjectManyAttribute>();
            foreach (var classType in classTypes)
                foreach (var interfaceType in classType.GetInterfaces())
                    container.RegisterType(interfaceType, classType, classType.Name);
        }

        private static void AddTypesInterfaces(IUnityContainer container, Type type)
        {
            foreach (var interfaceType in type.GetInterfaces())
                container.RegisterType(interfaceType, type);
        }
    }
}
