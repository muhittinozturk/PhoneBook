
using Domain.Entities;

namespace Application.Abstract
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<bool> AddAsync(T entity);
        IQueryable<T> GetAll();
        Task<T> GetByIdAsync(Guid id);
        bool Update(T entity);
        Task<bool> DeleteAsync(Guid id);
        Task<int> SaveAsync();

    }
}
