using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics.CodeAnalysis;

namespace RestApi.Shared.Query
{
    public class QueryResourceConverter : JsonConverter<QueryResource>
    {
        public override QueryResource ReadJson(JsonReader reader, Type objectType, [AllowNull] QueryResource existingValue, bool hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            var jObject = JToken.ReadFrom(reader);
            var resourceObject = GetResourceObject(jObject);
            if (resourceObject is LogicQueryResource)
                DeserializeQueryResources(jObject, resourceObject);
            return resourceObject;
        }

        private static QueryResource GetResourceObject(JToken jObject)
        {
            var json = jObject.ToString();
            Type resourceType = GetResourceType(jObject);
            var resourceObject = System.Text.Json.JsonSerializer.Deserialize(json, resourceType);
            return (QueryResource)resourceObject;
        }

        private static Type GetResourceType(JToken jObject)
        {
            var typeName = jObject[nameof(QueryResource.Type)].ToString();
            var queryResourceType = typeof(QueryResource);
            return Type.GetType($"{queryResourceType.Namespace}.{typeName},{queryResourceType.Assembly}");
        }

        private static void DeserializeQueryResources(JToken jObject, QueryResource resourceObject)
        {
            var jsonArray = jObject[nameof(LogicQueryResource.QueryResources)].ToString();
            (resourceObject as LogicQueryResource).QueryResources = JsonConvert.DeserializeObject<QueryResource[]>(jsonArray);
        }

        public override void WriteJson(JsonWriter writer, [AllowNull] QueryResource value, Newtonsoft.Json.JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}