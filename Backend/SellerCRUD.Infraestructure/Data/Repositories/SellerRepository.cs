using Microsoft.EntityFrameworkCore;
using SellerCRUD.Domain.Entities;
using SellerCRUD.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SellerCRUD.Infraestructure.Data.Repositories
{
    public class SellerRepository : GenericRepository<Seller>, ISellerRepository
    {
        public SellerRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
        }
        public async Task<List<Seller>> GetSellersAsync()
        {
            return await _appDbContext.Sellers
                    .Include(c => c.City)
                    .ToListAsync();
        }
    }
}
