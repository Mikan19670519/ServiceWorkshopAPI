using ServiceWorkshopAPI.Contexts;
using ServiceWorkshopAPI.Data.Contracts;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkshopAPI.Repositories
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly string _nameOrConnectionString;
        private readonly ServiceWorkshopDbContext _context;

        public IBookingsRepository BookingsRepository { get; }

        public RepositoryManager(string nameOrConnectionString)
        {
            _nameOrConnectionString = nameOrConnectionString;
            _context = new ServiceWorkshopDbContext(nameOrConnectionString);

            BookingsRepository = new BookingRepository(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public IServiceWorkshopDbContext CreateDbContext()
        {
            return new ServiceWorkshopDbContext(_nameOrConnectionString);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            int result = await _context.SaveChangesAsync();
            return result;
        }
    }
}
