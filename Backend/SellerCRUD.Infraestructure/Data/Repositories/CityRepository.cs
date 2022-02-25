using SellerCRUD.Domain.Entities;
using SellerCRUD.Domain.Interfaces;

namespace SellerCRUD.Infraestructure.Data.Repositories
{
    public class CityRepository : GenericRepository<City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext appDbContext) : base(appDbContext)
        {
        }
    }
}
