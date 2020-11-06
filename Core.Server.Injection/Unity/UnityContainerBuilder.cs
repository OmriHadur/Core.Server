using Core.Server.Injection.Attributes;
using Core.Server.Injection.Cache;
using Core.Server.Injection.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;

namespace Core.Server.Injection.Unity
{
    public class UnityContainerBuilder
    {
        private readonly IUnityContainer container;
        private readonly IReflactionHelper reflactionHelper;
        private readonly Dictionary<Type, object> factoryObjects;

        public UnityContainerBuilder(IUnityContainer container, IReflactionHelper reflactionHelper)
        {
            this.container = container;
            this.reflactionHelper = reflactionHelper;
            factoryObjects = new Dictionary<Type, object>();
        }
        public void ConfigureContainer()
        {
            AddAllTypesForBundles();
            AddInjectTypes<InjectAttribute>();
            AddInjectTypes<InjectOverridAttribute>();

            new UnityCacheBuilder().AddCache(container, reflactionHelper);
        }

        private void AddAllTypesForBundles()
        {
            var resourcesBoundles = reflactionHelper.GetResourcesBoundles();
            var genricTypesWithNameForBundle = GetInjectBoundleWithNameForBundle();

            foreach (var resourcesBoundle in resourcesBoundles)
                foreach (var genricTypeWithNameForBundle in genricTypesWithNameForBundle)
                    AddGenricTypeWithNameForBundle(genricTypeWithNameForBundle, resourcesBoundle);
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

        private void AddInjectTypes<TAttribute>()
            where TAttribute : Attribute
        {
            var classTypes = reflactionHelper.GetTypesWithAttribute<TAttribute>();
            foreach (var injectedType in classTypes)
                AddTypesInterfaces(injectedType);
        }

        private void InjectWithName(Type type, Type interfaceType)
        {
            var argsNames = type.GetGenericArguments().Select(t => t.Name);
            var argsNamesAsString = string.Join(",", argsNames);
            var name = $"{type.Name}<{argsNamesAsString}>";
            container.RegisterSingleton(interfaceType, type, name);
        }

        private void AddTypesInterfaces(Type type)
        {
            var interfacesType = reflactionHelper.GetDirectInterfaces(type);
            foreach (var interfaceType in interfacesType)
            {
                if (!interfaceType.IsGenericType || !type.IsGenericType)
                    RegisterType(type, interfaceType);
                else
                {
                    var interGenericType = interfaceType.GetGenericTypeDefinition();
                    var typeArgs = type.GetGenericArguments();
                    var interArgs = interfaceType.GetGenericArguments();
                    if (typeArgs.Length == interArgs.Length)
                        RegisterType(type, interGenericType);
                    else
                        RegisterFactory(type, interGenericType, typeArgs);
                }
            }
        }

        private void RegisterType(Type type, Type interfaceType)
        {
            container.RegisterSingleton(interfaceType, type);
            var name = type.Name;
            var indexOf = name.IndexOf('`');
            name = indexOf == -1 ? name : name.Substring(0, indexOf);
            container.RegisterSingleton(interfaceType, type, name);
        }

        private void RegisterFactory(Type type, Type interGenericType, Type[] typeArgs)
        {
            container.RegisterFactory(interGenericType, (uc, interTypeWithGeneric, obj) =>
            {
                if (factoryObjects.ContainsKey(interTypeWithGeneric))
                    return factoryObjects[interTypeWithGeneric];

                var typeGenericType = reflactionHelper.GetTypeGenericType(type, typeArgs, interTypeWithGeneric);
                var resolvedObject = uc.Resolve(typeGenericType);
                factoryObjects.Add(interTypeWithGeneric, resolvedObject);
                return resolvedObject;
            });
        }
    }
}