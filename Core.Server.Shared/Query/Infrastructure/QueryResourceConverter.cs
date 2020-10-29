using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Core.Server.Shared.Query
{
    public class QueryResourceConverter : JsonConverter<QueryPropertyResource>
    {
        public override QueryPropertyResource ReadJson(JsonReader reader, Type objectType, [AllowNull] QueryPropertyResource existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var jObject = JToken.ReadFrom(reader);
            var resourceObject = GetResourceObject(jObject);
            if (resourceObject is LogicQueryResource)
                DeserializeQueryResources(jObject, resourceObject);
            return resourceObject;
        }

        private static QueryPropertyResource GetResourceObject(JToken jObject)
        {
            var json = jObject.ToString();
            Type resourceType = GetResourceType(jObject);
            var resourceObject = System.Text.Json.JsonSerializer.Deserialize(json, resourceType);
            return (QueryPropertyResource)resourceObject;
        }

        private static Type GetResourceType(JToken jObject)
        {
            var typeName = jObject[nameof(QueryPropertyResource.Type)].ToString();
            var queryResourceType = typeof(QueryPropertyResource);
            return Type.GetType($"{queryResourceType.Namespace}.{typeName},{queryResourceType.Assembly}");
        }

        private static void DeserializeQueryResources(JToken jObject, QueryPropertyResource resourceObject)
        {
            var jsonArray = jObject[nameof(LogicQueryResource.QueryResources)].ToString();
            (resourceObject as LogicQueryResource).QueryResources = JsonConvert.DeserializeObject<QueryPropertyResource[]>(jsonArray);
        }

        public override void WriteJson(JsonWriter writer, [AllowNull] QueryPropertyResource value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}