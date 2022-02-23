using Microsoft.EntityFrameworkCore;
using SellerCRUD.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SellerCRUD.Infraestructure.Data.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext _appDbContext;

        public GenericRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        #region Métodos Sincrónicos

        public IEnumerable<TEntity> GetAll()
        {
            return _appDbContext.Set<TEntity>().ToList();
        }

        public ValueTask<TEntity> GetByIdAsync(int id)
        {
            return _appDbContext.Set<TEntity>().FindAsync(id);
        }

        public void Create(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Add(entity);
        }

        public void Update(TEntity entity)
        {
            _appDbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            _appDbContext.Set<TEntity>().Remove(entity);
        }

        #endregion

        #region Métodos Asincrónicos

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _appDbContext.Set<TEntity>().ToListAsync();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _appDbContext.Set<TEntity>().AddAsync(entity);
        }

        #endregion
    }
}
