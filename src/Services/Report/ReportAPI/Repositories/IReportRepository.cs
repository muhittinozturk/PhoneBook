using ReportAPI.Entities;
using System.Linq.Expressions;

namespace ReportAPI.Repositories
{
    public interface IReportRepository<T> where T : class
    {
        void Add(T entity);
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> filter);
        void Update(T entity);
        void Delete(Guid id);
    }
}
