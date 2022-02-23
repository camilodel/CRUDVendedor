using Microsoft.EntityFrameworkCore.Storage;
using SellerCRUD.Domain.Interfaces;
using System.Threading.Tasks;

namespace SellerCRUD.Infraestructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _appDbContext;
        private IDbContextTransaction _transaction;

        public ISellerRepository SellerRepository { get; }

        public UnitOfWork(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            SellerRepository = new SellerRepository(appDbContext);
        }

        public void Commit()
        {
            try
            {
                _appDbContext.SaveChanges();
                _transaction.Commit();
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                var result = await _appDbContext.SaveChangesAsync();
                _transaction.Commit();
                return result;
            }
            finally
            {
                _transaction.Dispose();
            }
        }

        public void BeginTransaction()
        {
            _transaction = _appDbContext.Database.BeginTransaction();
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _appDbContext.Database.BeginTransactionAsync();
        }

        public void CommitTransaction()
        {
            _appDbContext.Database.CommitTransaction();
        }

        public void RollbackTransaction()
        {
            _appDbContext.Database.RollbackTransaction();
        }
    }
}
