using Core.Server.Application.Helpers;
using Core.Server.Common;
using Core.Server.Common.Attributes;
using Core.Server.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity;

namespace Core.Server.Web.Utils
{
    public class UnityContainerBuilder
    {
        private IUnityContainer container;
        private IReflactionHelper reflactionHelper;
        public UnityContainerBuilder(IUnityContainer container, IReflactionHelper reflactionHelper)
        {
            this.container = container;
            this.reflactionHelper = reflactionHelper;
        }
        public void ConfigureContainer()
        {
            AddAllTypesForBundles();
            AddInjectTypes();
            //AddInjectWithNameClasses();
        }

        private void AddAllTypesForBundles()
        {
            var resourcesBoundles = reflactionHelper.GetResourcesBoundles();
            var genricTypesWithNameForBundle = GetInjectBoundleWithNameForBundle();

            foreach (var resourcesBoundle in resourcesBoundles) 
            {
                foreach (var genricTypeWithNameForBundle in genricTypesWithNameForBundle)
                    AddGenricTypeWithNameForBundle(genricTypeWithNameForBundle, resourcesBoundle);
            }
        }

        private void AddGenricTypeWithNameForBundle(Type genricTypeForBundle, ResourceBoundle resourcesBoundle)
        {
            var filledGenericType = reflactionHelper.FillGenericType(genricTypeForBundle, resourcesBoundle);
            foreach (var interfaceType in filledGenericType.GetInterfaces())
                InjectWithName(filledGenericType, interfaceType);
        }

        private IEnumerable<Type> GetInjectBoundleWithNameForBundle()
        {
            return reflactionHelper.GetGenericTypesWithAttribute<InjectBoundleWithNameAttribute>();
        }

        private void AddInjectTypes()
        {
            var classTypes = reflactionHelper.GetTypesWithAttribute<InjectAttribute>();
            foreach (var injectedType in classTypes)
                AddTypesInterfaces(injectedType);
        }

        private void InjectWithName(Type type,Type interfaceType)
        {
            var argsNames = type.GetGenericArguments().Select(t => t.Name);
            var argsNamesAsString= string.Join(",", argsNames);
            var name = $"{type.Name}<{argsNamesAsString}>";
            container.RegisterType(interfaceType, type, name);
        }

        private void AddTypesInterfaces(Type type)
        {
            foreach (var interType in type.GetInterfaces())
            {
                if (!interType.IsGenericType)
                    container.RegisterType(interType, type);
                else if (type.IsGenericType)
                {
                    var typeArgs = type.GetGenericArguments();
                    var interArgs= interType.GetGenericArguments();
                   if (typeArgs.Length== interArgs.Length)
                        container.RegisterType(interType, type);
                    else
                    {
                        container.RegisterFactory(interType, (uc, interTypeWithGeneric, obj) =>
                        {
                            var firstGen = interTypeWithGeneric.GetGenericArguments().First();
                            var prefix = reflactionHelper.GetPrefixName(firstGen);
                            var entityType = reflactionHelper.GetTypeWithPrefix<Entity>(prefix);
                            var args = interArgs.Union(new Type[] { entityType }).ToArray();
                            var typeGenericType = type.MakeGenericType(args);
                            return uc.Resolve(typeGenericType);
                        });
                    }
                }
            }
                
        }
    }
}