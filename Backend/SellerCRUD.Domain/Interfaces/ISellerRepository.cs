using SellerCRUD.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SellerCRUD.Domain.Interfaces
{
    public interface ISellerRepository : IGenericRepository<Seller>
    {
        Task<List<Seller>> GetSellersAsync();
    }
}
