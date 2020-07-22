using Microsoft.EntityFrameworkCore;
using ServiceWorkshopAPI.Data.Entities;

namespace ServiceWorkshopAPI.Models
{
    public class ServiceWorkshopContext : DbContext
    {
        public ServiceWorkshopContext(DbContextOptions<ServiceWorkshopContext> options) : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Vehicle> Vechicles { get; set; }

    }
}

