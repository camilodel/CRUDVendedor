using SellerCRUD.Domain.Entities;
using SellerCRUD.Domain.Interfaces;

namespace SellerCRUD.Infraestructure.Data.Repositories
{
    public class SellerRepository : GenericRepository<Seller>, ISellerRepository
    {
        public SellerRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
