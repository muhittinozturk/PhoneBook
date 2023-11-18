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
            var filter = Builders<T>.Filter.Eq("_id", GetIdValue(entity));

            var updateDefinition = Builders<T>.Update
                .Set("_id", GetIdValue(entity))
                .Set(x => x, entity);

            _collection.UpdateOne(filter, updateDefinition);
        }

        public void Delete(Guid id)
        {
            _collection.DeleteOne(Builders<T>.Filter.Eq("_id", id));
        }

        private Guid GetIdValue(T entity)
        {
            var idProperty = entity.GetType().GetProperty("_id");

            if (idProperty != null)
            {
                return (Guid)idProperty.GetValue(entity, null);
            }
            else
            {
                throw new ArgumentException("Entity must have _id property");
            }
        }
    }
}
