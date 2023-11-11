using MongoDB.Driver;
using ReportAPI.Data;
using ReportAPI.Entities;
using System.Linq.Expressions;

namespace ReportAPI.Repositories
{
    public class ReportRepository<T> : IReportRepository<T> where T : class
    {
        private readonly IMongoCollection<T> _collection;

        public ReportRepository(ReportDbContext<Report> dbContext)
        {
            _collection = (IMongoCollection<T>?)dbContext.Reports;
        }

        public void Add(T entity)
        {
            _collection.InsertOne(entity);
        }

        public T GetById(Guid id)
        {
            return _collection.Find(Builders<T>.Filter.Eq("_id", id)).FirstOrDefault();
        }

        public IEnumerable<T> GetAll()
        {
            return _collection.Find(_ => true).ToEnumerable();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> filter)
        {
            return _collection.Find(filter).ToEnumerable();
        }

        public void Update(T entity)
        {
            //İhtiyaç yok
        }

        public void Delete(Guid id)
        {
            _collection.DeleteOne(Builders<T>.Filter.Eq("_id", id));
        }
    }
}
