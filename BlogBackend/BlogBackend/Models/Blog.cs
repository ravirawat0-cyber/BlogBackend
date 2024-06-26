using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlogBackend.Models
{
    public class Blog
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("blogContent")]
        public string BlogContent { get; set; }

        [BsonElement("tags")]
        public List<string> Tags { get; set; }

        [BsonElement("createdOn")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [BsonElement("favourite")] public bool Favourite { get; set; } = false;
    }


    public class BlogRequest
    {
        public string Title { get; set; }
        public string BlogContent { get; set; }
        public List<string> Tags { get; set; }
    }
}
