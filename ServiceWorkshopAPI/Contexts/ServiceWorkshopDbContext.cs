using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ServiceWorkshopAPI.Data.Contracts;
using ServiceWorkshopAPI.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkshopAPI.Contexts
{
    internal class ServiceWorkshopDbContext : DbContext, IServiceWorkshopDbContext
    {
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }

        static ServiceWorkshopDbContext()
        {
        }
    }
}
