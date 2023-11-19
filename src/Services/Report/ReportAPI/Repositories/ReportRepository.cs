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

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate)
        {
            var report = await _collection.Find(predicate).FirstOrDefaultAsync();

            return report;
        }

        public IEnumerable<T> GetAll()
        {
            return _collection.Find(_ => true).ToEnumerable();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> filter)
        {
            return _collection.Find(filter).ToEnumerable();
        }

        public async Task<T> UpdateAsync(T updateEntity, Expression<Func<T, bool>> predicate)
        {
            var result = await _collection.FindOneAndReplaceAsync(predicate, updateEntity);
            return result;
        }

        public void Delete(string id)
        {
            _collection.DeleteOne(Builders<T>.Filter.Eq("_id", id));
        }

        private string GetIdValue(T entity)
        {
            var idProperty = entity.GetType().GetProperty("_id");

            if (idProperty != null)
            {
                return idProperty.GetValue(entity, null).ToString();
            }
            else
            {
                throw new ArgumentException("İstenen Id Değeri Bulunamadı");
            }
        }
    }
}
