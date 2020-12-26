using MongoDB.Bson;

namespace Core.Server.Common.Entities
{
    public class ChildEntity : Entity
    {
        public ChildEntity()
        {
            Id = ObjectId.GenerateNewId().ToString();
        }
    }
}
