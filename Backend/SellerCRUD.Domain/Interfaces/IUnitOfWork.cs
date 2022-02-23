using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellerCRUD.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ISellerRepository SellerRepository { get; }

        void Commit();
        Task<int> CommitAsync();
        void BeginTransaction();
        Task BeginTransactionAsync();
        void CommitTransaction();
        void RollbackTransaction();
    }
}
