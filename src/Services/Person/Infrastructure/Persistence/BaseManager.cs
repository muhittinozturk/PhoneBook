using Application.Abstract;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Persistence.Contexts;

namespace Infrastructure.Persistence
{
    public class BaseManager<T> : IBaseService<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext _context;

        public BaseManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();
        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> entry = await Table.AddAsync(entity);

            return entry.State == EntityState.Added;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            T entity = await GetByIdAsync(id);

            EntityEntry<T> entry = Table.Remove(entity);

            return entry.State == EntityState.Deleted;
        }

        public IQueryable<T> GetAll() => Table;
        public async Task<T> GetByIdAsync(Guid id) => await Table.FindAsync(id);

        public bool Update(T entity)
        {
            EntityEntry<T> entry = Table.Update(entity);
            return entry.State == EntityState.Modified;
        }

        public async Task<int> SaveAsync(CancellationToken token)
        {
            return await _context.SaveChangesAsync(token);
        }
    }
}
