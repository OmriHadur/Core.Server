using System.Reflection;

namespace Core.Server.Test.ResourceCreation.Interfaces
{
    public interface IObjectRandomizer
    {
        void AddRandomValues(object resource);
        string GetRandomId();
        string GetRandomString(int length);
        void SetRandomValue(object resource, PropertyInfo property);
        PropertyInfo GetRandomProperty<T>();
    }
}