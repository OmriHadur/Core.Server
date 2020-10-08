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
using Core.Server.Application.Mappers.Base;
using MongoDB.Bson.Serialization.Attributes;

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
            AddInjectWithNameClasses();
        }

        private void AddAllTypesForBundles()
        {
            var resourcesBoundles = reflactionHelper.GetResourcesBoundles().ToList();
            var genricTypesForBundle = GetGenricTypesForBundle(reflactionHelper).ToList();

            foreach (var genricTypeForBundle in genricTypesForBundle)
                foreach (var resourcesBoundle in resourcesBoundles)
                    AddGenricTypeResourcesBoundle(genricTypeForBundle, resourcesBoundle);
        }

        private void AddGenricTypeResourcesBoundle(Type genricTypeForBundle, ResourceBoundle resourcesBoundle)
        {
            var filledGenericType = reflactionHelper.FillGenericType(genricTypeForBundle, resourcesBoundle);
            AddTypesInterfaces(filledGenericType);
            if (HasInjectWithNameAttribute(genricTypeForBundle))
                InjectWithName(filledGenericType);
        }

        private static bool HasInjectWithNameAttribute(Type genricTypeForBundle)
        {
            return genricTypeForBundle.GetCustomAttribute<InjectWithNameAttribute>() != null;
        }

        private static IEnumerable<Type> GetGenricTypesForBundle(IReflactionHelper reflactionHelper)
        {
            return reflactionHelper.GetGenericTypesWithAttribute<InjectBoundleAttribute>();
        }

        private void AddInjectTypes()
        {
            var classTypes = reflactionHelper.GetTypesWithAttribute<InjectAttribute>();
            foreach (var injectedType in classTypes)
                AddTypesInterfaces(injectedType);
        }

        private void AddInjectWithNameClasses()
        {
            var classTypes = reflactionHelper.GetTypesWithAttribute<InjectWithNameAttribute>();
            foreach (var classType in classTypes.Where(t => !t.IsGenericType))
                InjectWithName(classType);
        }

        private void InjectWithName(Type type)
        {
            var injectMany = type.GetCustomAttribute<InjectWithNameAttribute>();
            container.RegisterType(injectMany.Type, type, type.Name);
        }

        private void AddTypesInterfaces(Type type)
        {
            foreach (var interfaceType in type.GetInterfaces())
                container.RegisterType(interfaceType, type);
        }
    }
}