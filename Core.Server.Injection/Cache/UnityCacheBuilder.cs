using Core.Server.Common.Cache;
using Unity;

namespace Core.Server.Injection.Cache
{
    public class UnityCacheBuilder
    {
        public void AddCache(IUnityContainer unityContainer)
        {
            var impType = typeof(CacheEntityConfig<>);
            var interfaceType = typeof(ICacheEntityConfig<>);

            unityContainer.RegisterType(interfaceType, impType);

            //var reflactionHelper = unityContainer.Resolve<ReflactionHelper>();
            //var cacheConfig = unityContainer.Resolve<CacheConfig>();
            //foreach (var configOverride in cacheConfig.Overrides)
            //{
            //    var overrideType = reflactionHelper.GetTypeByName(configOverride.Type);
            //    var interfaceOverridType = interfaceType.MakeGenericType(overrideType);

            //}
        }
    }
}
