using MongoDB.Driver;

namespace ReportAPI.Data;
public interface IReportDbContext<T> where T : class
{
    IMongoDatabase GetDatabase();
    IMongoCollection<T> Reports { get; }
}

