using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SellerCRUD.Domain.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        ValueTask<TEntity> GetByIdAsync(int id);
        TEntity GetSingle(object key);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task CreateAsync(TEntity entity);
    }
}
