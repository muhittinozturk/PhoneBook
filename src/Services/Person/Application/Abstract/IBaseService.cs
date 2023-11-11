
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.Abstract
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(Guid id, Expression<Func<T, object>>? expression = null);
        bool Update(T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<int> SaveAsync(CancellationToken token);

    }
}
