using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ReportAPI.Enums;

namespace ReportAPI.Entities
{
    public class Report
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } 

        public DateTime RequestedAt { get; set; } = DateTime.UtcNow; 

        public ReportStatus Status { get; set; }
    }
}
