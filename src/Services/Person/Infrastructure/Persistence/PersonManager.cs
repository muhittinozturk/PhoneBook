using Application.Abstract;
using Domain.Entities;
using Infrastructure.Persistence;
using Persistence.Contexts;

namespace Persistence.Concrete
{
    public class PersonManager : BaseManager<Person>, IPersonService
    {
        private readonly ApplicationDbContext _context;
        public PersonManager(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
