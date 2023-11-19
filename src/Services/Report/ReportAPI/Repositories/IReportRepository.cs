using ReportAPI.Entities;
using System.Linq.Expressions;

namespace ReportAPI.Repositories
{
    public interface IReportRepository<T> where T : class
    {
        void Add(T entity);
        Task<T> GetByIdAsync(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> filter);
        Task<T> UpdateAsync(T updateEntity, Expression<Func<T, bool>> predicate);
        void Delete(string id);
    }
}
