using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ServiceWorkshopAPI.Contexts;
using ServiceWorkshopAPI.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkshopAPI.Repositories
{
    internal abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity> _set;

        protected DbSet<TEntity> Set => _set ?? (_set = Context.Set<TEntity>());

        protected ServiceWorkshopDbContext Context { get; }

        protected Repository(ServiceWorkshopDbContext context)
        {
            Context = context;
        }

        public List<TEntity> GetAll()
        {
            return Set.ToList();
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return Set.ToListAsync();
        }

        public TEntity FindById(object id)
        {
            return Set.Find(id);
        }

        public Task<TEntity> FindByIdAsync(object id)
        {
            return Set.FindAsync(id);
        }

        public void Add(TEntity entity)
        {
            Set.Add(entity);
        }

        public void Update(TEntity entity)
        {
            EntityEntry<TEntity> entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                Set.Attach(entity);
                entry = Context.Entry(entity);
            }
            entry.State = EntityState.Modified;
        }

        public void Remove(TEntity entity)
        {
            Set.Remove(entity);
        }
    }
}
