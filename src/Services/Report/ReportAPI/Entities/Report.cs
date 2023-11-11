using MongoDB.Bson.Serialization.Attributes;
using ReportAPI.Enums;

namespace ReportAPI.Entities
{
    public class Report
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.String)]
        public Guid Id { get; set; }

        public DateTime RequestedAt { get; set; }

        public ReportStatus Status { get; set; }
    }
}
