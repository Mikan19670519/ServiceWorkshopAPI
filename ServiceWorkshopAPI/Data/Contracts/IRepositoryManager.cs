using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkshopAPI.Data.Contracts
{
    public interface IRepositoryManager : IDisposable
    {
        IBookingsRepository BookingsRepository { get; }

        IServiceWorkshopDbContext CreateDbContext();

        int SaveChanges();
        Task<int> SaveChangesAsync();
    }
}
