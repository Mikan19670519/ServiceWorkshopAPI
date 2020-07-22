using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceWorkshopAPI.Data.Contracts
{
    public interface IRepository<TEntity> where TEntity : class
    {
        List<TEntity> GetAll();
        Task<List<TEntity>> GetAllAsync();
        TEntity FindById(object id);
        Task<TEntity> FindByIdAsync(object id);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove(TEntity entity);
    }
}
